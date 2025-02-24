using UnityEngine;

public class EnemyLaser : Bullet
{
    public float laserDuration;
    private float laserTimer;

    protected override void Despawn()
    {
        Destroy(gameObject);
    }

    protected override void AdvanceProjectile()
    {
        if (UIManager.instance.GetCurrentActiveUI() != UIManager.GameUI.InGame) return;

        laserTimer += Time.deltaTime;
        if (laserTimer > laserDuration)
        {
            Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().DoDamage(bulletSettings.Damage);
        }
    }

}