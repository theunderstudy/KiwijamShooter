using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet_Enemy : MonoBehaviour
{
    public int maxBounces = 0;
    public float speed;
    private int bounces = 0;
    [SerializeField] Rigidbody2D rb;


    private void OnCollisionEnter(Collision collision)
    {
        bounces++;
        if (bounces >= maxBounces + 1)
            Destroy(gameObject);
    }

    public void BulletGo()
    {
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

}
