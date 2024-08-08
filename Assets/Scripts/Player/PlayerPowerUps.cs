using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowerUps : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Transform firingPosition;
    [SerializeField] private GameObject bullet;

    private int bulletSpeed;
    private bool isAttacking;

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
        playerInputActions.Movement.Attack.performed += IsFiringBullets;
        playerInputActions.Movement.Attack.canceled += IsFiringBullets;

        playerInputActions.Movement.Shield.Enable();
        playerInputActions.Movement.Shield.performed += OnShieldActive;

        PowerUp.speedPowerUp += SpeedPowerUp;
        PowerUp.damagePowerUp += DamagePowerUp;
        PowerUp.healthPowerUp += HealthPowerUp;
        PowerUp.shieldCooldownReductionPowerUp += ShieldCooldownReduction;
    }

    private void OnDestroy()
    {
        playerInputActions.Movement.Attack.performed -= IsFiringBullets;
        playerInputActions.Movement.Attack.canceled -= IsFiringBullets;
        playerInputActions.Movement.Attack.Disable();

        playerInputActions.Movement.Shield.performed -= OnShieldActive;
        playerInputActions.Movement.Shield.Disable();

        PowerUp.speedPowerUp -= SpeedPowerUp;
        PowerUp.damagePowerUp -= DamagePowerUp;
        PowerUp.healthPowerUp -= HealthPowerUp;
        PowerUp.shieldCooldownReductionPowerUp -= ShieldCooldownReduction;
    }

    private void Start()
    {
        playerSavedStatus = PlayerSaveSystem.Instance.CurrentPlayerStatus;

        attackDelay = 0f;
        shieldCooldownTimer = 0f;
        activeShieldTimer = 0.5f;
        bulletSpeed = 50;

        attackSpeed = playerSavedStatus.playerAttackSpeed;
        attackDamage = playerSavedStatus.playerDamage;
        shieldCooldown = playerSavedStatus.playerShieldCooldown;
    }

    private void Update()
    {
        attackDelay -= Time.deltaTime;
        shieldCooldownTimer -= Time.deltaTime;

        FireWaterBullet();
    }

    private void IsFiringBullets(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;
        }
        else if (context.canceled)
        {
            isAttacking = false;
        }
    }

    private void FireWaterBullet()
    {
        if (isAttacking)
        {
            if (attackDelay < 0)
            {
                GameObject waterBullet = Instantiate(bullet, firingPosition.position, transform.rotation);

                waterBullet.GetComponent<Bullet>().BulletSpeed = bulletSpeed;
                waterBullet.GetComponent<Bullet>().BulletDamage = attackDamage;
                waterBullet.GetComponent<Bullet>().TargetTag = "Enemy";


                attackDelay = attackSpeed;
            }
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
        int damageToIncrease = 1;

        int newAttackDamage = attackDamage + damageToIncrease;

        if (!(newAttackDamage > 6))
        {
            attackDamage = newAttackDamage;
            playerSavedStatus.playerDamage = attackDamage;

            PlayerSaveSystem.Instance.SaveData(playerSavedStatus);
        }
    }

    private void SpeedPowerUp()
    {
        float attackSpeedIncrease = 0.1f;

        float newAttackSpeed = attackSpeed - attackSpeedIncrease;

        if (!(newAttackSpeed < 0.2f))
        {
            attackSpeed = newAttackSpeed;
            playerSavedStatus.playerAttackSpeed = attackSpeed;

            PlayerSaveSystem.Instance.SaveData(playerSavedStatus);
        }
    }

    private void HealthPowerUp()
    {
        int hpToIncrease = 1;

        GetComponent<Player>().IncreaseHealth(hpToIncrease);
    }

    private void ShieldCooldownReduction()
    {
        float cooldownReduction = 0.5f;

        float newShiedlCooldown = shieldCooldown - cooldownReduction;

        if (!(newShiedlCooldown < 0.5f))
        {
            shieldCooldown -= cooldownReduction;
            playerSavedStatus.playerShieldCooldown = shieldCooldown;
            
            PlayerSaveSystem.Instance.SaveData(playerSavedStatus);
        }
    }

}
