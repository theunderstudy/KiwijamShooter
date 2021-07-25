using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyBase
{
    public float TimeBetweenAttacks = 2;
    private float AttackTimer;

    protected override void Awake()
    {
        base.Awake();
        AttackTimer = TimeBetweenAttacks;
        

    }

    protected override void Attack()
    {
        if (AttackTimer>= TimeBetweenAttacks)
        {
            GameManager.instance.player.GetHit();
            AttackTimer = 0;
        }
        else
        {
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }
    protected override void Update()
    {
        base.Update();
        CurrentState = AIState.moving;
        AttackTimer += Time.deltaTime;
        if (CurrentState == AIState.moving)
        {
            MoveToPlayer();
        }
        else if (CurrentState == AIState.attacking)
        {
            Attack();
        }
    }
}
