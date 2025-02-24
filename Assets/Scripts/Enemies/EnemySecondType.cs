using System.Collections;
using UnityEngine;

public class EnemySecondType : Enemy
{
    // KAMIKAZE ENEMY - EXPLODE NEAR THE PLAYER
    [SerializeField] private float explosionTimer;
    [SerializeField] private int damageToPlayer;

    private PlayerController playerController;
    private bool playerInSight;

    protected override void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    protected override void Update()
    {
        if (UIManager.instance.GetCurrentActiveUI() != UIManager.GameUI.InGame) return;
        Move();
        CheckForHealth();
    }

    protected override void SetDirection()
    {
        direction = (playerController.transform.position - transform.position).normalized;
    }

    protected override void Shoot()
    {
        StartCoroutine(ExplosionRoutine());
    }

    #region MOVEMENT
    private void Move()
    {
        if (!playerInSight)
        {
            SetDirection();
            transform.Translate(speed * Time.deltaTime * direction);
        }
    }
    #endregion

    #region FIRING
    public IEnumerator ExplosionRoutine()
    {
        float t = 0;

        while (t < explosionTimer)
        {
            t += Time.deltaTime;
            yield return null;
        }

        if (t > explosionTimer)
        {
            playerController?.DoDamage(damageToPlayer);
            Destroy(gameObject);
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = FindAnyObjectByType<PlayerController>();
            playerInSight = true;
            Shoot();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = null;
        }
    }


}
