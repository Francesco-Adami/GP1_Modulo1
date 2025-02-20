using UnityEngine;

public class EnemyFirstType : Enemy
{
    protected override void SetDirection()
    {
        float randomY = Random.Range(-_screenBounds.y, _screenBounds.y);
        float randomX = Random.Range(0, _screenBounds.x - 1f);
        direction = new Vector2(randomX, randomY);
    }

    protected override void Shoot()
    {
        // istantiate bullet


        canShoot = false;
        StartCoroutine(ShootRoutine(fireRate));
    }
}
