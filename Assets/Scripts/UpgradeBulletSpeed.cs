using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBulletSpeed : Upgrade
{

    protected override void UpgradePlayer()
    {
        GameManager.instance.player.UpgradeBulletSpeed();
    }
}
