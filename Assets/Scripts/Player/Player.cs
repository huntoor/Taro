using UnityEngine;

public class Player : MonoBehaviour
{
    private float health;

    private void Awake()
    {
        Bullet.damageTarget += TakeDamage;
    }

    private void Start()
    {
        health = 10;
    }

    private void Respawn()
    {

    }
    
    private void TakeDamage(int damage, Collider2D myCollider)
    {
        if (myCollider == GetComponent<Collider2D>())
        {
            health -= damage;
            
            Debug.Log("Decrease Player HP by " + damage);

            if (health <= 0)
            {
                Debug.Log("Player Dead");
            }

            Die();
        }
    }

    private void Die()
    {

    }
}
