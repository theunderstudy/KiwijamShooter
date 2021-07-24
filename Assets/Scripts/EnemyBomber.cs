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
}
