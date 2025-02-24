using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float speed;

    protected Vector2 direction;
    protected Vector2 _screenBounds;
    protected bool canShoot;

    protected virtual void Start()
    {
        _screenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        SetDirection();
        canShoot = true;
    }

    protected virtual void Update()
    {
        if (UIManager.instance.GetCurrentActiveUI() != UIManager.GameUI.InGame) return;

        if (direction == Vector2.zero || direction == null) SetDirection();

        MoveEnemy(direction);
        CheckEnemyArrived();

        CheckForHealth();
    }

    #region MOVEMENT
    private void CheckEnemyArrived()
    {
        if (Vector2.Distance(transform.position, direction) < 0.5f)
        {
            SetDirection();
        }
    }

    protected void MoveEnemy(Vector2 direction)
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
    }
    #endregion

    #region FIRING
    protected void TryShoot()
    {
        if (!canShoot) return;

        Shoot();
    }

    protected IEnumerator ShootRoutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        canShoot = true;
    }
    #endregion

    public void DoDamage(int damage) { health -= damage; }
    protected void CheckForHealth() { if (health <= 0) Destroy(gameObject); }

    protected abstract void SetDirection(); // set with different patterns
    protected abstract void Shoot();

}
