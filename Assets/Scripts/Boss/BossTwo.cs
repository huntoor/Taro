using UnityEngine;

public class BossTwo : BaseBoss
{

    private void Update()
    {
        if (attackDelay >= 0)
        {
            attackDelay -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
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
            ShootRandomBullet();
        }
    }

    private void ShootRandomBullet()
    {
        GameObject randomBullet = bullets[Random.Range(0, bullets.Length)];
        
        GameObject bulletInstance = Instantiate(randomBullet, firingPosition.position, transform.rotation);

        if (bulletInstance.TryGetComponent(out Bullet _))
        {
            Bullet bullet = bulletInstance.GetComponent<Bullet>();

            bullet.BulletSpeed = bulletSpeed;
            bullet.BulletDamage = bulletDamage / 2;
            bullet.TargetTag = "Player";
            bullet.Player = player;


            attackDelay = attackSpeed;
        }
        else if (bulletInstance.TryGetComponent(out ExplosiveBullet _))
        {
            ExplosiveBullet bullet = bulletInstance.GetComponent<ExplosiveBullet>();
            
            bullet.BulletSpeed = bulletSpeed;
            bullet.BulletDamage = bulletDamage;
            bullet.BulletChaseTimer = bulletChaseTimer;
            bullet.TargetTag = "Player";
            bullet.Player = player;
            bullet.TargetMask = 1 << player.layer;

            attackDelay = attackSpeed;
        }
    }
}