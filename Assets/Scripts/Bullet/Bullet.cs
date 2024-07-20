using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    private float bulletLifeSpan;

    public float BulletSpeed { get; set; }

    public string TargetTag { get; set; }

    public int BulletDamage { get; set; }

    public delegate void DamageTarget(int damage);
    public static DamageTarget damageTarget;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        bulletLifeSpan = 3f;
    }

    private void FixedUpdate()
    {
        bulletLifeSpan -= Time.deltaTime;

        if (bulletLifeSpan < 0)
        {
            Destroy(gameObject);
        }

        MoveBullet();
    }

    private void MoveBullet()
    {
        myRigidBody.velocity = new Vector2(BulletSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.CompareTag(TargetTag))
        {
            damageTarget?.Invoke(BulletDamage);
            
            Destroy(gameObject);
        }
    }
}
