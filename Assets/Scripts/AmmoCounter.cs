using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    public Image BulletIcon;
    private Gun PlayerGun;
    public Transform IconTransform;

    public List<Image> BulletIcons;

    // Start is called before the first frame update
    void Start()
    {
        PlayerGun = GameManager.instance.player.playerGun;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        int ShotsRemaining = PlayerGun.CurrentMagazineCount;
        int Capacity = PlayerGun.MagazineCapacity;

        // Check capacity display

        if (IconTransform.childCount < PlayerGun.MagazineCapacity)
        {
            // Add icon
            Image icon = Instantiate(BulletIcon);
            icon.rectTransform.SetParent(IconTransform.GetComponent<RectTransform>());
            BulletIcons.Add(icon);
        }

        if (IconTransform.childCount  > PlayerGun.MagazineCapacity)
        {
            Image remove = BulletIcons[BulletIcons.Count - 1];
            // remove icon
            remove.transform.parent = null;
            BulletIcons.Remove(remove) ;
            Destroy(remove);
        }

        for (int i = 0; i < BulletIcons.Count; i++)
        {
            if (i < ShotsRemaining)
            {
                BulletIcons[i].color = Color.white;
            }
            else
            {
                BulletIcons[i].color = Color.black;
            }
        }



    }
}
