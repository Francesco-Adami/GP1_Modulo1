using UnityEngine;

[CreateAssetMenu(fileName = "BulletStat", menuName = "ScriptableObject/BulletStat")]
public class BulletSettings : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private bool canPierce; // goes through enemies

    public float Speed { get { return speed; } }
    public int Damage { get { return damage; } }
    public bool CanPierce { get { return canPierce; } }
}
