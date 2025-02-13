using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected BulletSettings bulletSettings;
    protected Vector2 direction = Vector2.zero;

    protected abstract void AdvanceProjectile();

    public void SetDirection(Vector2 direction) { this.direction = direction; }
}
