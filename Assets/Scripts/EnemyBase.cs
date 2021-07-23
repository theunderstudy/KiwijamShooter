using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is meant to be abstract but not for testing
public class EnemyBase : MonoBehaviour
{
    public int Health = 1;
    public Rigidbody2D RB;
    protected Collider2D Collider2D;

    protected virtual void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void GetHit()
    {
        Debug.Log("Hit");
        Health -= 1;
        if (Health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Collider2D.enabled = false;
        
    }
}
