using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    public int health = 6;
    [SerializeField] private float speed;
    [SerializeField] private Image playerHealth;

    [Header("Player Firing")]
    [SerializeField] private float fireRate; // time to shoot the next bullet
    [SerializeField] private float minLoadingTimer;
    [SerializeField] public float maxLoadingTimer;

    // movement
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _translation;
    private Vector2 _screenBounds;
    private float _playerWidth, _playerHeight;

    // firing
    private bool canShoot = true;
    private bool mouseHasBeenPressed = false;
    [HideInInspector] public float loadedBullet;

    // SINGLETON
    public static PlayerController instance { get; set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        canShoot = true;

        // screen limits
        _screenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // half of the player sizes
        _playerWidth = GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
        _playerHeight = GetComponentInChildren<SpriteRenderer>().bounds.extents.y;
    }

    private void Update()
    {
        if (UIManager.instance.GetCurrentActiveUI() != UIManager.GameUI.InGame) return;

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        Move();
        CheckShoot();
    }

    #region MOVEMENT
    private void Move()
    {
        _translation = speed * Time.deltaTime * new Vector2(_horizontalInput, _verticalInput).normalized;
        transform.Translate(_translation);

        // keep the player inside the screen
        KeepPlayerInBounds();
    }

    private void KeepPlayerInBounds()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -_screenBounds.x + _playerWidth, _screenBounds.x - _playerWidth);
        float clampedY = Mathf.Clamp(transform.position.y, -_screenBounds.y + _playerHeight, _screenBounds.y - _playerHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
    #endregion

    #region FIRING
    private void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0)) mouseHasBeenPressed = true;
        if (!canShoot) return;

        if (Input.GetMouseButton(0) && mouseHasBeenPressed)
        {
            loadedBullet += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0) && mouseHasBeenPressed)
        {
            if (loadedBullet < minLoadingTimer)
            {
                ShootBullet(BulletType.Basic);
            }
            else if (loadedBullet >= minLoadingTimer && loadedBullet < maxLoadingTimer)
            {
                ShootBullet(BulletType.Charged1);
            }
            else
            {
                ShootBullet(BulletType.Charged2);
            }
            loadedBullet = 0;
            mouseHasBeenPressed = false;
        }
    }

    private void ShootBullet(BulletType bulletType)
    {
        // shoot
        bulletsManager.instance.InstantiateBullet(bulletType, transform.position, ShooterType.Player);
        canShoot = false;
        StartCoroutine(ShootRoutine(fireRate));
    }

    private IEnumerator ShootRoutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        canShoot = true;
    }
    #endregion

    #region SETTERS
    public void DoDamage(int damage) { health -= damage; SetShieldTime(); }

    public void SetShieldTime() => playerHealth.fillAmount = (1f / 6f) * health;
    #endregion
}
