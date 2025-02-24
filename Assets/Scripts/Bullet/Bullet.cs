using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected BulletSettings bulletSettings;
    protected Vector2 direction = Vector2.zero;

    private void Update()
    {
        AdvanceProjectile();
    }

    protected abstract void AdvanceProjectile();
    protected abstract void Despawn();

    public void SetDirection(Vector2 direction) { this.direction = direction; }

}
