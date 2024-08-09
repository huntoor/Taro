using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    private float bulletLifeSpan;

    private Vector2 playerDirection;

    public float BulletSpeed { get; set; }

    public string TargetTag { get; set; }

    public int BulletDamage { get; set; }

    public bool IsPlayerTarget { get; set; }
    public GameObject Player { get; set;}
    

    public delegate void DamageTarget(int damage, Collider2D body);
    public static DamageTarget damageTarget;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        bulletLifeSpan = 3f;

        if (TargetTag == "Player" && Player != null)
        {
            playerDirection = Player.transform.position - transform.position;   
        }
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
        if (TargetTag == "Player" && Player != null)
        {
            myRigidBody.velocity = new Vector2(playerDirection.x, playerDirection.y).normalized * BulletSpeed;

            float rotation = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0, rotation);
        }
        else
        {
            myRigidBody.velocity = new Vector2(BulletSpeed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.CompareTag(TargetTag))
        {
            damageTarget?.Invoke(BulletDamage, body);
            
            Destroy(gameObject, 0.05f);
        }
    }
}
