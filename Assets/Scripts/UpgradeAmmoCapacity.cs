using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAmmoCapacity : Upgrade
{


    protected override void UpgradePlayer()
    {
        GameManager.instance.player.UpgradeAmmoCapacity();
    }
}

