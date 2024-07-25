using Unity.Play.Publisher.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Transform firingPosition;
    [SerializeField] private GameObject bullet;

    private enum State
    {
        Idle,
        Attack
    }

    private State currentState;
    private State CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;

            StateChanged();
        }
    }

    private GameObject player;

    private int bulletSpeed;
    private int bulletDamage;

    private float attackDelay;

    private int health;

    private void OnEnable()
    {
        Bullet.damageTarget += TakeDamage;
    }

    private void OnDisable()
    {
        Bullet.damageTarget -= TakeDamage;
    }

    private void Start()
    {
        CurrentState = State.Idle;

        player = null;

        bulletSpeed = 20;
        bulletDamage = 1;
        attackDelay = 1.5f;

        health = 3;
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
                Shoot();
            }
        }
    }

    private void StateChanged()
    {
        switch (CurrentState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Attack:
                AttackState();
                break;
            default:
                Debug.LogError("Error Something Worng with Enemy State");
                break;
        }
    }

    private void IdleState()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void AttackState()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void Shoot()
    {
        if (attackDelay < 0)
        {
            GameObject bullet = Instantiate(this.bullet, firingPosition.position, transform.rotation);

            bullet.GetComponent<Bullet>().BulletSpeed = bulletSpeed;
            bullet.GetComponent<Bullet>().BulletDamage = bulletDamage;
            bullet.GetComponent<Bullet>().TargetTag = "Player";
            bullet.GetComponent<Bullet>().Player = player;


            attackDelay = 1.5f;
        }
    }

    public void OnPlayerEneted(Collider2D player)
    {
        this.player = player.gameObject;

        CurrentState = State.Attack;
    }

    private void TakeDamage(int damageToTake, Collider2D myCollider)
    {
        if (myCollider == GetComponent<Collider2D>())
        {
            health -= damageToTake;

            if (health <= 0)
            {
                Debug.Log("Enemy Dead");
                Destroy(gameObject);
            }
        }
    }
}
