using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFireRate : Upgrade
{
    public override void DowngradePlayer()
    {
        GameManager.instance.player.UpgradeFireRate(false);

    }

    protected override void UpgradePlayer()
    {
        GameManager.instance.player.UpgradeFireRate(true);

        AttachToPlayer();
    }
}
