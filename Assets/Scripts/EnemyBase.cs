using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public enum AIState
{
    nill = 0,
    moving = 100,
    attacking = 200,
    dying = 300
}
public abstract class EnemyBase : MonoBehaviour
{
    public int Health = 1;
    public Rigidbody2D RB;
    protected PlayerController player;
    protected Collider2D Collider2D;
    protected AIState CurrentState = AIState.nill;

    public float maxSpeed;
    public float acc;

    protected virtual void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
        player = GameManager.instance.player;
        
    }


    protected virtual void UpdateAIState()
    {
        // Check if in attack range
        // -> atk
        // else move?
        // maybe some 3rd state
    }

    protected virtual void MoveToPlayer()
    {
        // Move to player
        // Stop at attack range        
    }

    protected abstract void Attack();

    public virtual void GetHit()
    {
        if (Dying())
        {
            return;
        }

        Health -= 1;
        if (Health <= 0)
        {
            StartDeath();
        }
    }

    protected virtual void StartDeath()
    {
        CurrentState = AIState.dying;
        transform.DOScale(Vector2.zero, 1.0f).SetEase(Ease.InBack).OnComplete(Die);
        Collider2D.enabled = false;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public bool Dying() 
    {

        return CurrentState == AIState.dying;
    }


}
