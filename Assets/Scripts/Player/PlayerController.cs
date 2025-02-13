using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float fireRate; // time to shoot the next bullet
    [SerializeField] private float minLoadingTimer;
    [SerializeField] private float maxLoadingTimer;

    // movement
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _translation;

    // firing
    private bool canShoot = true;
    private float loadedBullet;

    private void Start()
    {
        canShoot = true;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        Move();
        CheckShoot();
    }

    #region MOVEMENT
    private void Move()
    {
        _translation = speed * Time.deltaTime * new Vector2(_horizontalInput, _verticalInput).normalized;
        transform.Translate(_translation);

        // Check if is in screen

        // Ottieni i limiti dello schermo in unità del mondo
        // Vector2 screenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        // float playerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        // float playerHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
        // clamp position
    }
    #endregion

    #region SHOOTING
    private void CheckShoot()
    {
        if (!canShoot) return;

        if (Input.GetMouseButton(0))
        {
            loadedBullet += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (loadedBullet < minLoadingTimer)
            {
                ShootBullet(BulletType.Basic);
            }
            else if (loadedBullet >= minLoadingTimer && loadedBullet < maxLoadingTimer)
            {
                ShootBullet(BulletType.Charged1);
            }
            else
            {
                ShootBullet(BulletType.Charged2);
            }
            loadedBullet = 0;
        }
    }

    private void ShootBullet(BulletType bulletType)
    {
        // shoot
        bulletsManager.instance.InstantiateBullet(bulletType, transform.position, ShooterType.Player);
        canShoot = false;
        StartCoroutine(ShootRoutine(fireRate));
    }

    private IEnumerator ShootRoutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        canShoot = true;
    }
    #endregion
}
