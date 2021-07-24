using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBulletSpeed : Upgrade
{
    public override void DowngradePlayer()
    {
        GameManager.instance.player.UpgradeBulletSpeed(false);

    }

    protected override void UpgradePlayer()
    {
        GameManager.instance.player.UpgradeBulletSpeed(true);

        AttachToPlayer();
    }
}
