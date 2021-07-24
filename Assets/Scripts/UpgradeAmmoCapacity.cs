using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAmmoCapacity : Upgrade
{


    public override void DowngradePlayer()
    {
        GameManager.instance.player.UpgradeAmmoCapacity(false);

    }

    protected override void UpgradePlayer()
    {
        GameManager.instance.player.UpgradeAmmoCapacity(true);

        AttachToPlayer();
    }
}

