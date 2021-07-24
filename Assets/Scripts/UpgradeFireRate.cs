using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFireRate : Upgrade
{
    protected override void UpgradePlayer()
    {
        GameManager.instance.player.UpgradeFireRate();
    }
}
