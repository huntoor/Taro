using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] protected Transform firingPosition;
    [SerializeField] protected GameObject bullet;

    [Space]

    [Header("Enemy States")]
    [SerializeField] protected int health;
    [SerializeField] protected int bulletSpeed;
    [SerializeField] protected int bulletDamage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float bulletChaseTimer;

    protected float attackDelay;
    public delegate void OnEnemyDeath();
    public static OnEnemyDeath onEnemyDeath;

    protected enum State
    {
        Idle,
        Attack
    }

    protected State currentState;
    protected State CurrentState
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

    protected GameObject player;

    private Animator animator;

    protected void OnEnable()
    {
        BaseBullet.damageTarget += TakeDamage;
    }

    protected void OnDestroy()
    {
        BaseBullet.damageTarget -= TakeDamage;
    }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        attackDelay = attackSpeed;
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

    public void OnPlayerEneted(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            this.player = player.gameObject;

            CurrentState = State.Attack;
        }
    }

    private void TakeDamage(int damageToTake, Collider2D myCollider)
    {
        if (myCollider == GetComponent<Collider2D>())
        {
            health -= damageToTake;

            StartCoroutine(HitEffect());

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator HitEffect()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);
        
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void Die()
    {
        onEnemyDeath?.Invoke();

        Destroy(gameObject);
    }

    protected void FireAttackAnimation()
    {
        animator.SetTrigger("shoot");
    }

    protected abstract void IdleState();

    protected abstract void AttackState();

    protected abstract void Shoot();
}
