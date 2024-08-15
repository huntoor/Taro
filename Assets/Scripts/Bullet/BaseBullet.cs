using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseBullet : MonoBehaviour
{
    protected Rigidbody2D myRigidBody;

    protected float bulletLifeSpan;

    protected Vector2 playerDirection;

    public float BulletSpeed { get; set; }

    public string TargetTag { get; set; }

    public int BulletDamage { get; set; }

    public bool IsPlayerTarget { get; set; }
    public GameObject Player { get; set;}

    public delegate void DamageTarget(int damage, Collider2D body);
    public static DamageTarget damageTarget;

    protected abstract void MoveBullet();
}
