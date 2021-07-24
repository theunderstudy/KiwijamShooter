using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : EnemyBase
{
    
    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Attack()
    {
        Explode();
    }

    private void Explode()
    {

    }

    protected override void Update()
    {
        base.Update();

        if(CurrentState == AIState.moving)
        {
            MoveToPlayer();
        }
        else if(CurrentState == AIState.attacking)
        {
            Attack();
        }

    }
}
