using System;
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

        playerInputActions.Movement.Attack.Enable();
        playerInputActions.Movement.Attack.performed += FireWaterBullet;

        attackDelay = 0f;
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
}
