using UnityEngine;

public class PlayerBullet : Bullet
{
    void Update()
    {
        AdvanceProjectile();
    }

    protected override void AdvanceProjectile()
    {
        transform.Translate(bulletSettings.Speed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
