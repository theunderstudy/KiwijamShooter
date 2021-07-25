using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePenetration : Upgrade
{


    public override void DowngradePlayer()
    {
        GameManager.instance.player.UpgradePenetration(false);

    }

    protected override void UpgradePlayer()
    {
        GameManager.instance.player.UpgradePenetration(true);

        AttachToPlayer();
    }
}
