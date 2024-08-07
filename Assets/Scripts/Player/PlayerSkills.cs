using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkills : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Transform firingPosition;
    [SerializeField] private GameObject bullet;

    private int bulletSpeed;
    private int bulletDamage;

    private float attackDelay;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        attackDelay = 0f;
    }

    private void OnEnable()
    {
        playerInputActions.Movement.Attack.Enable();
        playerInputActions.Movement.Attack.performed += FireWaterBullet;

        PowerUp.speedPowerUp += SpeedPowerUp;
        PowerUp.damagePowerUp += DamagePowerUp;
        PowerUp.healthPowerUp += HealthPowerUp;

        Skill.blastSkill += BlastSkill;
        Skill.rapidFireSkill += RapidFireSkill;
        Skill.laserSkill += LaserSkill;
    }

    private void Start()
    {
        bulletSpeed = 50;
        bulletDamage = 1;
    }

    private void Update()
    {
        attackDelay -= Time.deltaTime;
    }

    private void FireWaterBullet(InputAction.CallbackContext context)
    {
        if (attackDelay < 0)
        {
            Debug.Log("Fire Bullet");
            GameObject waterBullet = Instantiate(bullet, firingPosition.position, transform.rotation);

            waterBullet.GetComponent<Bullet>().BulletSpeed = bulletSpeed;
            waterBullet.GetComponent<Bullet>().BulletDamage = bulletDamage;
            waterBullet.GetComponent<Bullet>().TargetTag = "Enemy";


            attackDelay = 0.3f;
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

    private void RapidFireSkill()
    {
        Debug.Log("Rapid Fire");
    }

    private void BlastSkill()
    {
        Debug.Log("Blast Skill");

    }

    private void LaserSkill()
    {
        Debug.Log("Laser Skill");
    }
}
