using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public TopDownCharacterController Player;
    public Transform IconTransform;
    public Image HPIcon;

    public Sprite[] HPSprites;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.instance.player;
        for (int i = 0; i < Player.Health; i++)
        {
            Image add = Instantiate(HPIcon);
            add.rectTransform.SetParent( IconTransform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }


    void UpdateDisplay()
    {
        if (IconTransform.childCount > Player.Health && IconTransform.childCount> 0)
        {
            // Remove icon
           Transform remove = IconTransform.GetChild(IconTransform.childCount -1);
            remove.SetParent( null);
            Destroy(remove.gameObject);
        }
        if (IconTransform.childCount < Player.Health)
        {
            // Add icon
            Image add = Instantiate(HPIcon);
            add.rectTransform.SetParent(IconTransform);
            add.sprite = HPSprites[Random.Range(0, HPSprites.Length)];
            add.rectTransform.rotation = Quaternion.Euler( 0,0,Random.Range(-180,180));
        }



    }
}
