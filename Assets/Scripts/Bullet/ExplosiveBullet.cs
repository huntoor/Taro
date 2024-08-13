using UnityEngine;

public class ExplosiveBullet : BaseBullet
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private float explosiveRadius = 2;
    [SerializeField] private float force = 20;

    private CircleCollider2D myCollider;
    private Collider2D[] affectedColliders;


    public LayerMask TargetMask { get; set; }

    private bool _isExploded;
    private bool IsExploded
    {
        get { return _isExploded; }
        set
        {
            if (_isExploded != value)
            {
                _isExploded = value;

                GetComponent<SpriteRenderer>().enabled = !_isExploded;
            }
        }
    }

    private float bulletChaseTimer;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();

        explosionParticle.Stop();
    }

    private void Start()
    {
        bulletLifeSpan = 3f;
        bulletChaseTimer = 1f;
        IsExploded = false;

        if (TargetTag == "Player" && Player != null)
        {
            playerDirection = Player.transform.position - transform.position;
        }
    }

    private void FixedUpdate()
    {
        bulletLifeSpan -= Time.deltaTime;
        bulletChaseTimer -= Time.deltaTime;

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

        IsExploded = true;

        affectedColliders = Physics2D.OverlapCircleAll(transform.position, explosiveRadius, TargetMask);

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
        if (!IsExploded)
        {
            if (TargetTag == "Player" && Player != null)
            {
                if (bulletChaseTimer > 0)
                {
                    playerDirection = Player.transform.position - transform.position;
                }

                myRigidBody.velocity = new Vector2(playerDirection.x, playerDirection.y).normalized * BulletSpeed;

                float rotation = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotation);

                if (IsBulletCollision())
                {
                    Explode();
                }
            }
            else
            {
                myRigidBody.velocity = new Vector2(BulletSpeed, 0);
            }
        }
        else
        {
            myRigidBody.velocity = Vector2.zero;
        }
    }

    private bool IsBulletCollision()
    {
        return Physics2D.OverlapCircle(transform.position, myCollider.radius, TargetMask) != null;
    }
}
