using UnityEngine;

public class ExplosiveEnemy : BaseEnemy
{
    private void Start()
    {
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
            GameObject bullet = Instantiate(this.bullet, firingPosition.position, transform.rotation);

            bullet.GetComponent<ExplosiveBullet>().BulletSpeed = bulletSpeed;
            bullet.GetComponent<ExplosiveBullet>().BulletDamage = bulletDamage;
            bullet.GetComponent<ExplosiveBullet>().TargetTag = "Player";
            bullet.GetComponent<ExplosiveBullet>().Player = player;

            attackDelay = attackSpeed;
        }
    }
}
