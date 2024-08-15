using UnityEngine;

public abstract class BaseBoss : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] protected Transform firingPosition;
    [SerializeField] protected GameObject[] bullets;

    [Space]

    [Header("Boss States")]
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

    protected State _currentState;
    protected State CurrentState
    {
        get
        {
            return _currentState;
        }

        set
        {
            _currentState = value;

            StateChanged();
        }
    }

    protected GameObject player;

    protected void OnEnable()
    {
        BaseBullet.damageTarget += TakeDamage;
    }

    protected void OnDestroy()
    {
        BaseBullet.damageTarget -= TakeDamage;
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
                Debug.LogError("Error Something Worng with Boss State");
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
            Debug.Log("Boss Hit");
            health -= damageToTake;

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        onEnemyDeath?.Invoke();

        Destroy(gameObject);
    }

    protected abstract void IdleState();

    protected abstract void AttackState();

    protected abstract void Shoot();
}
