using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyRanged : EnemyBase
{

    public float bulletCooldown;
    public float bulletSpeed;
    public int bulletBounces;
    public Bullet_Enemy bullet;
    public Transform bulletSpawnPoint;
    public float strafeStrength = 8f;
    private float bulletTimer;
    private int timesFired;
    private int timesFiredWithoutMoving = 4;

    protected override void Attack()
    {
        if (bulletTimer < 0)
        {
            Bullet_Enemy bulletClone = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, null);
            bulletClone.maxBounces = bulletBounces;
            bulletClone.speed = bulletSpeed;
            bulletClone.BulletGo();
            bulletTimer = bulletCooldown;
            bulletTimer += Random.Range(bulletCooldown * 0.2f, bulletCooldown * 0.6f);
            /*timesFired++;
            timesFiredWithoutMoving = Random.Range(1, 6);
            if(timesFired > timesFiredWithoutMoving)
            {
                timesFired = 0;

                RB.AddForce(transform.up * Mathf.Abs(Random.Range(-5, 5)) * strafeStrength, ForceMode2D.Impulse);
            }*/
            
        }
        else
        {

            bulletTimer -= Time.deltaTime;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (CurrentState == AIState.attacking)
        {
            Attack();
        }
        else if (CurrentState == AIState.moving)
        {
            MoveToPlayer();
        }
        Vector2 playerPos = player.transform.position;
        Vector2 lookDir = playerPos - RB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Random.Range(0, 0.2f));
    }
}
