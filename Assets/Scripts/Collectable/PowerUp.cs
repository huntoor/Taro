using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private enum PowerUpType
    {
        Speed,
        Damage,
        Health,
        ShieldCooldown
    }

    [SerializeField] private PowerUpType powerUpType;

    public delegate void DamagePowerUp();
    public static DamagePowerUp damagePowerUp;

    public delegate void SpeedPowerUp();
    public static SpeedPowerUp speedPowerUp;

    public delegate void HealthPowerUp();
    public static HealthPowerUp healthPowerUp;

    public delegate void ShieldCooldownReductionPowerUp();
    public static ShieldCooldownReductionPowerUp shieldCooldownReductionPowerUp;

    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.CompareTag("Player"))
        {
            switch (powerUpType)
            {
                default:

                case PowerUpType.Speed:
                    speedPowerUp?.Invoke();
                    break;

                case PowerUpType.Damage:
                    damagePowerUp?.Invoke();
                    break;

                case PowerUpType.Health:
                    healthPowerUp?.Invoke();
                    break;

                case PowerUpType.ShieldCooldown:
                    shieldCooldownReductionPowerUp?.Invoke();
                    break;
            }

            Destroy(gameObject);
        }
    }
}
