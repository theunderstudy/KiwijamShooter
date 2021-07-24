using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet_Enemy : MonoBehaviour
{
    [HideInInspector]
    public int maxBounces = 0;
    [HideInInspector]
    public float speed;
    private int bounces = 0;
    [SerializeField] Rigidbody2D rb;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounces++;
        if (bounces >= maxBounces)
            Destroy(gameObject); // this should probably happen later
    }

    public void BulletGo()
    {
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

}
