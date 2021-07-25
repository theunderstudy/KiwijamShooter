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

    protected override void Update()
    {
        base.Update();
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
