using System.Collections;
using UnityEngine;

public class EnemyThirdType : Enemy
{
    // RANDOM ENEMY - BASE SHOOT
    [SerializeField] protected float fireRate;
    [SerializeField] protected Bullet bullet;

    private bool firstTimeWait;
    private float t;
    private bool isShooting;

    protected override void Start()
    {
        base.Start();
        canShoot = false;
    }

    protected override void Update()
    {
        if (UIManager.instance.GetCurrentActiveUI() != UIManager.GameUI.InGame) return;

        if (!firstTimeWait)
        {
            t += Time.deltaTime;
            if (t > fireRate)
            {
                firstTimeWait = true;
                canShoot = true;
            }
        }
        else
        {
            TryShoot();
        }

        if (isShooting) return;
        base.Update();
    }

    protected override void SetDirection()
    {
        float randomY = Random.Range(-_screenBounds.y, _screenBounds.y);
        float x = _screenBounds.x - 1f;
        direction = new Vector2(x, randomY);
    }

    protected override void Shoot()
    {
        isShooting = true;
        Transform shootPos = FindAnyObjectByType<PlayerController>().transform;

        EnemyLaser _tmp = Instantiate((EnemyLaser)bullet, transform.position, Quaternion.identity);
        _tmp.SetDirection(shootPos.position - transform.position);

        canShoot = false;
        StartCoroutine(ShootRoutine(fireRate));
        StartCoroutine(StopMovingRoutine(_tmp.laserDuration));
    }

    private IEnumerator StopMovingRoutine(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
        isShooting = false;
    }
}
