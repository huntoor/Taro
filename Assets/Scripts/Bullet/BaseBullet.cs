using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseBullet : MonoBehaviour
{
    protected Rigidbody2D myRigidBody;
    protected Animator animator;

    protected float bulletLifeSpan;

    protected Vector2 playerDirection;


    public float BulletSpeed { get; set; }

    public string TargetTag { get; set; }

    public int BulletDamage { get; set; }

    public bool IsPlayerTarget { get; set; }
    public GameObject Player { get; set;}

    public delegate void DamageTarget(int damage, Collider2D body);
    public static DamageTarget damageTarget;

    protected virtual void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected void DestoryBullet()
    {
        Destroy(gameObject);
    }

    protected void FireBulletSound()
    {
        SoundManager.Instance.Play("FireBullet");
    }

    protected void FireExplosiveBulletSound()
    {
        SoundManager.Instance.Play("BulletExplode");
    }

    protected void FireHitBulletSound()
    {
        SoundManager.Instance.Play("BulletHit");
    }

    protected abstract void MoveBullet();
}
