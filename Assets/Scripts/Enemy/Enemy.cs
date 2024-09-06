using UnityEngine;

public class Enemy : BaseEnemy
{
    protected override void Start()
    {
        base.Start();
        CurrentState = State.Idle;

        player = null;
    }

    private void Update()
    {
        attackDelay -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (CurrentState == State.Attack)
        {
            if (player != null)
            {
                Shoot();
            }
        }
    }

    protected override void IdleState()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    protected override void AttackState()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    protected override void Shoot()
    {
        if (attackDelay < 0)
        {
            FireAttackAnimation();
            
            GameObject bulletInstance = Instantiate(this.bullet, firingPosition.position, transform.rotation);
            Bullet bullet = bulletInstance.GetComponent<Bullet>();

            bullet.BulletSpeed = bulletSpeed;
            bullet.BulletDamage = bulletDamage;
            bullet.TargetTag = "Player";
            bullet.Player = player;


            attackDelay = attackSpeed;
        }
    }
}
