using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : EnemyBase
{
    
    protected override void Awake()
    {
        base.Awake();
        CurrentState = AIState.moving;
    }

    protected override void Attack()
    {
        Explode();
    }

    private void Explode()
    {

    }

    private void Move()
    {

        Vector2 playerPos = GameManager.instance.player.transform.position;
        Vector2 myPos = transform.position;
        Vector2 target = playerPos - myPos;
        Debug.DrawLine(myPos, myPos + target.normalized * 5);
        RB.AddForce(target.normalized * acc, ForceMode2D.Force );
        RB.velocity = Vector2.ClampMagnitude(RB.velocity, maxSpeed);
        
    }

    private void Update()
    {
        if(CurrentState == AIState.moving)
            Move();
    }
}
