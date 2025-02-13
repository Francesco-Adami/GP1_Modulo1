using System.Collections.Generic;
using UnityEngine;

public class bulletsManager : MonoBehaviour
{
    [SerializeField] private List<Bullet> bullets = new List<Bullet>();

    // singleton
    public static bulletsManager instance { get; private set; }

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

    public void InstantiateBullet(BulletType bulletType, Vector2 spawnPoint, ShooterType shooterType)
    {
        if (shooterType == ShooterType.Player)
        {
            Bullet bullet = Instantiate(bullets[(int)bulletType], spawnPoint, Quaternion.identity);
            bullet.SetDirection(Vector2.right);
        }
    }
}
