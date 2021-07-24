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
    private float bulletTimer;



    protected override void Attack()
    {
        if (bulletTimer < 0)
        {
            Bullet_Enemy bulletClone = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, null);
            bulletClone.maxBounces = bulletBounces;
            bulletClone.speed = bulletSpeed;
            bulletClone.BulletGo();
            bulletTimer = bulletCooldown; 
        }
        else
        {
            bulletTimer -= Time.deltaTime;
        }
    }
}
