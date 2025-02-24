using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void AdvanceProjectile()
    {
        if (UIManager.instance.GetCurrentActiveUI() != UIManager.GameUI.InGame) return;

        transform.Translate(bulletSettings.Speed * Time.deltaTime * direction.normalized);
    }

    protected override void Despawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().DoDamage(bulletSettings.Damage);
            if (!bulletSettings.CanPierce) Despawn();
        }

        if (collision.CompareTag("OutMap"))
        {
            Despawn();
        }
    }
}