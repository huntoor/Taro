using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ExplosiveEnemy : BaseEnemy
{
    [SerializeField] private float enemySpeed;

    private Rigidbody2D myRigidBody;

    private int movementDirection;
    private float halfEnemySizeY;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        CurrentState = State.Idle;

        player = null;
        movementDirection = 1;

        halfEnemySizeY = GetComponent<SpriteRenderer>().bounds.size.y / 2;

    }

    private void Update()
    {
        attackDelay -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (CurrentState == State.Attack)
        {
            if (player != null)
            {
                EnemyMovement();

                Shoot();
            }
        }
    }

    protected override void IdleState()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    protected override void AttackState()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    protected override void Shoot()
    {
        if (attackDelay < 0)
        {
            GameObject bulletInstance = Instantiate(this.bullet, firingPosition.position, transform.rotation);
            ExplosiveBullet bullet = bulletInstance.GetComponent<ExplosiveBullet>();
            
            bullet.BulletSpeed = bulletSpeed;
            bullet.BulletDamage = bulletDamage;
            bullet.TargetTag = "Player";
            bullet.Player = player;
            bullet.TargetMask = 1 << player.layer;

            attackDelay = attackSpeed;
        }
    }

    private void EnemyMovement()
    {
        Vector3 enemyPos = transform.position;
        
        SwitchDirection(enemyPos);

        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, movementDirection * enemySpeed);
    }

    private void SwitchDirection(Vector3 pos)
    {
        float distance = pos.z - Camera.main.transform.position.z;

        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + halfEnemySizeY;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance)).y - halfEnemySizeY;

        if (pos.y >= topBorder && pos.y >= bottomBorder)
        {
            movementDirection = -1;
        }
        else if (pos.y <= bottomBorder && pos.y <= topBorder)
        {
            movementDirection = 1;
        }

        CheckCollision();
    }

    private void CheckCollision()
    {
        float extraStartingHight = 2f;
        float rayDistance = 0.15f;

        if (movementDirection == 1)
        {
            if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + extraStartingHight), Vector2.up, rayDistance).collider != null)
            {
                movementDirection = -1;
            }
        }
        else if (movementDirection == -1)
        {
            if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - extraStartingHight), Vector2.down, rayDistance).collider != null)
            {
                movementDirection = -1; 
            }
        }
    }
}
