using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : EnemyBase
{
    public float blastRadius = 2;
    [SerializeField] LayerMask blastHitLayer;
    
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
        Vector2 pos = transform.position;
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(pos, blastRadius, blastHitLayer);
        foreach(Collider2D c in hitObjects)
        {
            if (c.GetComponent<TopDownCharacterController>())
            {
                c.GetComponent<TopDownCharacterController>().GetHit();
            }
            else if (c.GetComponent<EnemyBase>())
            {
                c.GetComponent<EnemyBase>().GetHit();
            }
        }
        StartDeath();
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
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
