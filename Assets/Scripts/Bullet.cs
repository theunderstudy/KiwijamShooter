using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    // Also does penetration
    public int BounceCount = 1;
    public Rigidbody2D RB;
    private Vector2 lastVelocity;


    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = RB.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion desired = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = desired;// Quaternion.RotateTowards(transform.rotation, desired, 1000 * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        lastVelocity = RB.velocity;
    }

    public void Init(Vector2 playerVelocity, float speed, int bounceCount = 1)
    {
        this.BounceCount = bounceCount;
        //RB.velocity = playerVelocity;
        RB.AddForce(transform.up * speed , ForceMode2D.Impulse);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BounceCount -= 1;
       

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponentInParent<EnemyBase>();
            if (enemy != null)
            {
                enemy.GetHit();
            }
            RB.velocity = lastVelocity;
        }
        else
        {

            Vector3 velocity = lastVelocity;
            Vector3 collisionNormal = collision.GetContact(0).normal;
            RB.velocity = Vector3.Reflect(velocity, collisionNormal);
        }

        if (BounceCount <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    
}
