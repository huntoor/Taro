using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    private int maxHealth;
    private int currentHealth;

    public bool IsInvincible { private get; set; } = false;

    private void Awake()
    {
        Bullet.damageTarget += TakeDamage;
    }

    private void Start()
    {
        maxHealth = GetComponent<PlayerSaveSystem>().CurrentPlayerStatus.playerMaxHealth;
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
                healthBar.SetHealth(currentHealth);
                
                Debug.Log("Decrease Player HP by " + damage);

                if (currentHealth <= 0)
                {
                    Debug.Log("Player Dead");
                }

                Die();
            }
        }
    }

    public void IncreaseHealth(int hp)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += hp;
            healthBar.SetHealth(currentHealth);
        }
    }

    private void Die()
    {

    }
}
