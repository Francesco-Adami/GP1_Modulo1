using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // hanno vita ? 
    [SerializeField] protected EnemyType enemyType;
    [SerializeField] protected float speed;
    [SerializeField] protected float fireRate;

    protected Vector2 direction;
    protected Vector2 _screenBounds;
    protected bool canShoot;

    private void Start()
    {
        _screenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        SetDirection();
        canShoot = true;
    }

    private void Update()
    {
        if (UIManager.instance.GetCurrentActiveUI() != UIManager.GameUI.InGame) return;

        if (direction == Vector2.zero || direction == null) SetDirection();

        MoveEnemy(direction);
        CheckEnemyArrived();

        TryShoot();
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


    protected abstract void SetDirection(); // set with different patterns
    protected abstract void Shoot();

}
