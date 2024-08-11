using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    private int maxHealth;
    private int currentHealth;

    public bool IsInvincible { private get; set; } = false;

    public delegate void SetMaxHealth(int maxHealth);
    public static SetMaxHealth setMaxHealth;

    public delegate void UpdateHealth(int currentHealth);
    public static UpdateHealth updateHealth;

    public delegate void PlayerDead();
    public static PlayerDead playerDead;

    private void OnEnable()
    {
        BaseBullet.damageTarget += TakeDamage;
    }

    private void OnDestroy()
    {
        BaseBullet.damageTarget -= TakeDamage;
    }

    private void Start()
    {
        maxHealth = PlayerSaveSystem.Instance.CurrentPlayerStatus.playerMaxHealth;
        
        currentHealth = maxHealth;
        setMaxHealth?.Invoke(maxHealth);
        //healthBar.SetMaxHealth(maxHealth);
    }

    private void Respawn()
    {

    }
    
    private void TakeDamage(int damage, Collider2D myCollider)
    {
        if (!IsInvincible)
        {
            if (myCollider == GetComponent<Collider2D>())
            {
                currentHealth -= damage;
                updateHealth?.Invoke(currentHealth);
                //healthBar.SetHealth(currentHealth);
                
                if (currentHealth <= 0)
                {
                    // Debug.Log("Player Dead");
                    
                    Die();
                }
            }
        }
    }

    public void IncreaseHealth(int hp)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += hp;
            updateHealth?.Invoke(currentHealth);
            //healthBar.SetHealth(currentHealth);
        }
    }

    private void Die()
    {
        playerDead?.Invoke();
    }
}
