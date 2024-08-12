using UnityEngine;

public class ExplosiveBullet : BaseBullet
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private float radius = 2;
    [SerializeField] private float force = 20;

    [SerializeField] private LayerMask layerMask;
    private Collider2D[] affectedColliders;


    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        explosionParticle.Stop();
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

    // for testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }

    private void Explode()
    {
        float cameraShakeIntenisty = 3f;
        float cameraShakeTimer = 0.6f;
        CameraShake.Instance.ShakeCamera(cameraShakeIntenisty, cameraShakeTimer);
        explosionParticle.Play();

        affectedColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach (Collider2D affectedCollider in affectedColliders)
        {
            if (affectedCollider.gameObject.TryGetComponent(out Rigidbody2D rb))
            {
                Vector2 forceDirection = (rb.transform.position - transform.position).normalized;

                damageTarget?.Invoke(BulletDamage, affectedCollider);

                rb.MovePosition(rb.position + (force * forceDirection));
            }
        }
    }
    protected override void MoveBullet()
    {
        if (TargetTag == "Player" && Player != null)
        {
            playerDirection = Player.transform.position - transform.position;

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
//        Debug.Log(TargetTag);
        if (body.CompareTag(TargetTag))
        {
            Explode();
        }
    }
}
