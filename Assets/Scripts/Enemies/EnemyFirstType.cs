using UnityEngine;

public class EnemyFirstType : Enemy
{

    // RANDOM ENEMY - BASE SHOOT
    [SerializeField] protected float fireRate;
    [SerializeField] protected Bullet bullet;

    protected override void Update()
    {
        if (UIManager.instance.GetCurrentActiveUI() != UIManager.GameUI.InGame) return;

        base.Update();
        TryShoot();
    }


    protected override void SetDirection()
    {
        float randomY = Random.Range(-_screenBounds.y, _screenBounds.y);
        float randomX = Random.Range(0, _screenBounds.x - 1f);
        direction = new Vector2(randomX, randomY);
    }

    protected override void Shoot()
    {
        // istantiate bullet
        Transform shootPos = FindAnyObjectByType<PlayerController>().transform;

        Bullet _tmp = Instantiate(bullet, transform.position, Quaternion.identity);
        _tmp.SetDirection(shootPos.position - transform.position);

        canShoot = false;
        StartCoroutine(ShootRoutine(fireRate));
    }
}
