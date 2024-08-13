using UnityEngine;

public class Bullet : BaseBullet
{
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

    protected override void MoveBullet()
    {
        if (TargetTag == "Player" && Player != null)
        {
            myRigidBody.velocity = new Vector2(playerDirection.x, playerDirection.y).normalized * BulletSpeed;

            float rotation = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotation);
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
            // float cameraShakeIntenisty = 1f;
            // float cameraShakeTimer = 0.1f;
            // CameraShake.Instance.ShakeCamera(cameraShakeIntenisty, cameraShakeTimer);

            damageTarget?.Invoke(BulletDamage, body);

            Destroy(gameObject);
        }
    }
}
