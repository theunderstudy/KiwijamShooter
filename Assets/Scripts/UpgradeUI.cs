using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Text AmmocapText;
    public Text FireRateText;
    public Text BulletSpeedText;
    public Text PenetrationText;
    private Gun playerGun;

    private void Start()
    {
        playerGun = GameManager.instance.player.playerGun;
    }


    private void Update()
    {
        AmmocapText.text = playerGun.MagazineCapacity.ToString();
        FireRateText.text = playerGun.FireRate.ToString("F2");
        BulletSpeedText.text = playerGun.BarrelSpeed.ToString("F2");
        PenetrationText.text = playerGun.BounceCount.ToString();
    }
}
