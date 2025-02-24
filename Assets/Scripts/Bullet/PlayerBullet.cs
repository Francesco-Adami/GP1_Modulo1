using UnityEngine;

public class PlayerBullet : Bullet
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
        if (collision.CompareTag("Enemy"))
        {
            // TODO - Deal damage
            collision.GetComponent<Enemy>().DoDamage(bulletSettings.Damage);

            if (!bulletSettings.CanPierce)
            {
                Despawn();
            }
        }

        if (collision.CompareTag("Boss"))
        {
            // TODO - Deal damage
            collision.GetComponent<Boss>().DoDamage(bulletSettings.Damage);
            Despawn();
        }

        if (collision.CompareTag("OutMap"))
        {
            Despawn();
        }
    }
}
