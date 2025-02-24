using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] private int health;
    [SerializeField] private int bulletPerFiring;
    [SerializeField] private float fireRate;
    [SerializeField] private float cooldownTimer;

    [Header("FIRING")]
    [SerializeField] private BossShootingType shootingType;
    [SerializeField] private GameObject firePoints;
    [SerializeField] private EnemyBullet bullet;

    private bool isCooldownOn;
    private int bulletShooted;
    private bool isfireRateOn;

    private bool randomActive;

    private void Start()
    {
        StartCooldown();
    }

    private void Update()
    {
        if (UIManager.instance.GetCurrentActiveUI() != UIManager.GameUI.InGame) return;

        CheckIfDefeated();

        if (isCooldownOn) return;
        Shoot();
    }

    public void DoDamage(int damage)
    {
        health -= damage;
    }

    private void CheckIfDefeated()
    {
        if (health <= 0)
        {
            GameManager.instance.bossDefeated = true;
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        if (isfireRateOn) return;

        switch (shootingType)
        {
            case BossShootingType.Basic:
                ShootBasic();
                break;
            case BossShootingType.Rotating:
                ShootRotating();
                break;
            default:
                break;
        }
    }

    #region BASIC
    private void ShootBasic()
    {
        if (bulletShooted < bulletPerFiring)
        {
            isfireRateOn = true;
            StartCoroutine(ShootRoutine());
        }
        else
        {
            StartCooldown();
            bulletShooted = 0;
        }
    }

    private IEnumerator ShootRoutine()
    {
        foreach (Transform firePoint in firePoints.transform)
        {
            EnemyBullet eb = Instantiate(bullet, firePoint.position, Quaternion.identity);
            eb.SetDirection(-firePoint.transform.right);
        }
        bulletShooted++;
        yield return new WaitForSeconds(fireRate);
        isfireRateOn = false;
    }
    #endregion

    #region ROTATING
    private void ShootRotating()
    {
        if (bulletShooted < bulletPerFiring)
        {
            isfireRateOn = true;
            StartCoroutine(RotatingRoutine());
        }
        else
        {
            StartCooldown();
            bulletShooted = 0;
            randomActive = !randomActive;
        }
    }

    private IEnumerator RotatingRoutine()
    {
        int i = randomActive ? 0 : 1;
        foreach (Transform firePoint in firePoints.transform)
        {
            if (i % 2 == 0)
            {
                EnemyBullet eb = Instantiate(bullet, firePoint.position, Quaternion.identity);
                eb.SetDirection(-firePoint.transform.right);
                i++;
            }
            else
            {
                i++;
                continue;
            }
        }
        bulletShooted++;
        yield return new WaitForSeconds(fireRate);
        isfireRateOn = false;
    }

    #endregion

    private void StartCooldown()
    {
        isCooldownOn = true;
        StartCoroutine(CooldownRoutine());
        shootingType = shootingType == BossShootingType.Basic ? BossShootingType.Rotating : BossShootingType.Basic;
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldownTimer);
        isCooldownOn = false;
    }
}
