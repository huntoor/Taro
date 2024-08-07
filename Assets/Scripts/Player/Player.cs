using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    private int maxHealth = 10;
    private int currentHealth;

    private void Awake()
    {
        Bullet.damageTarget += TakeDamage;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Respawn()
    {

    }
    
    private void TakeDamage(int damage, Collider2D myCollider)
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
