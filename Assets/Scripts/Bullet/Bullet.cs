using System.Collections;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected BulletSettings bulletSettings;
    [SerializeField] protected float despawnTime = 5f;
    protected Vector2 direction = Vector2.zero;

    private void Start()
    {
        StartCoroutine(DespawnRoutine());
    }

    protected abstract void AdvanceProjectile();

    public void SetDirection(Vector2 direction) { this.direction = direction; }

    protected IEnumerator DespawnRoutine()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
