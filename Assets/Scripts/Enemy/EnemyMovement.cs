using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private LayerMask collisionMask;

    [Space]
    [Header("Forward Movement")]
    [SerializeField] private bool isMovingForward;
    [SerializeField] private float forwawrdMovementAmount;

    private Rigidbody2D myRigidBody;

    private float movementDirection;
    private float halfEnemySizeY;

    private GameObject player;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        movementDirection = 1;

        halfEnemySizeY = GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            MoveEnemy();
        }
    }

    private void MoveEnemy()
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

            if (isMovingForward)
            {
                MoveForward();
            }
        }
        else if (pos.y <= bottomBorder && pos.y <= topBorder)
        {
            movementDirection = 1;

            if (isMovingForward)
            {
                MoveForward();
            }
        }

        CheckCollision();
    }

    private void CheckCollision()
    {
        float extraStartingHight = halfEnemySizeY;
        float rayDistance = 0.15f;

        if (movementDirection == 1)
        {
            if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + extraStartingHight), Vector2.up, rayDistance, collisionMask).collider != null)
            {
                movementDirection = -1;

                if (isMovingForward)
                {
                    MoveForward();
                }
            }
        }
        else if (movementDirection == -1)
        {
            if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - extraStartingHight), Vector2.down, rayDistance, collisionMask).collider != null)
            {
                movementDirection = 1;

                if (isMovingForward)
                {
                    MoveForward();
                }
            }
        }
    }

    public void OnPlayerEneted(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            this.player = player.gameObject;
        }
    }

    private void MoveForward()
    {
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;

            if (playerPosition.x > transform.position.x)
            {
                transform.position = new Vector2(transform.position.x + forwawrdMovementAmount, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - forwawrdMovementAmount, transform.position.y);
            }

        }
    }
}
