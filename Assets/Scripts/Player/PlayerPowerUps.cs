using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowerUps : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Transform firingPosition;
    [SerializeField] private GameObject bullet;

    private int bulletSpeed;

    private float activeShieldTimer;
    private float shieldCooldownTimer;

    // Player Status
    private float attackSpeed;
    private int attackDamage;
    private float shieldCooldown;


    private float attackDelay;

    private PlayerStatus playerSavedStatus;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Movement.Attack.Enable();
        playerInputActions.Movement.Attack.performed += FireWaterBullet;

        playerInputActions.Movement.Shield.Enable();
        playerInputActions.Movement.Shield.performed += OnShieldActive;

        PowerUp.speedPowerUp += SpeedPowerUp;
        PowerUp.damagePowerUp += DamagePowerUp;
        PowerUp.healthPowerUp += HealthPowerUp;
        PowerUp.shieldCooldownReductionPowerUp += ShieldCooldownReduction;
    }

    private void Start()
    {
        playerSavedStatus = GetComponent<PlayerSaveSystem>().CurrentPlayerStatus;

        attackDelay = 0f;
        shieldCooldownTimer = 0f;
        activeShieldTimer = 0.5f;
        bulletSpeed = 50;

        attackSpeed = playerSavedStatus.playerAttackSpeed;
        attackDamage = playerSavedStatus.playerDamage;
        shieldCooldown = playerSavedStatus.playerShieldCooldown;

        Debug.Log(playerSavedStatus.playerMaxHealth);
    }

    private void Update()
    {
        attackDelay -= Time.deltaTime;
        shieldCooldownTimer -= Time.deltaTime;
    }

    private void FireWaterBullet(InputAction.CallbackContext context)
    {
        if (attackDelay < 0)
        {
            Debug.Log("Fire Bullet");
            GameObject waterBullet = Instantiate(bullet, firingPosition.position, transform.rotation);

            waterBullet.GetComponent<Bullet>().BulletSpeed = bulletSpeed;
            waterBullet.GetComponent<Bullet>().BulletDamage = attackDamage;
            waterBullet.GetComponent<Bullet>().TargetTag = "Enemy";


            attackDelay = attackSpeed;
        }
    }

    private void OnShieldActive(InputAction.CallbackContext context)
    {
        StartCoroutine(nameof(ActivateShield));
    }

    private IEnumerator ActivateShield()
    {
        if (shieldCooldownTimer <= 0)
        {
            Debug.Log("activate Shield");
            GetComponent<Player>().IsInvincible = true;
            Color oldColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = Color.white;

            yield return new WaitForSeconds(activeShieldTimer);

            Debug.Log("Deactivate Shield");
            GetComponent<SpriteRenderer>().color = oldColor;
            GetComponent<Player>().IsInvincible = false;
            
            shieldCooldownTimer = shieldCooldown;
        }
    }

    private void DamagePowerUp()
    {
        Debug.Log("Damage Power Up");
    }

    private void SpeedPowerUp()
    {
        Debug.Log("Speed Power Up");
    }

    private void HealthPowerUp()
    {
        int hpToIncrease = 1;

        GetComponent<Player>().IncreaseHealth(hpToIncrease);
    }

    private void ShieldCooldownReduction()
    {
        float cooldownReduction = 0.5f;

        shieldCooldown -= cooldownReduction;
    }

}
