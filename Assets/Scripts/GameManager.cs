using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TopDownCharacterController player;
    public int GameStage = 1;
    public int MaxGameStage = 10;

    public Upgrade[] UpgradePrefabs;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        player = FindObjectOfType<TopDownCharacterController>();

    }

    public void SpawnUpgrade(Vector2 position)
    {
        Upgrade upgrade = Instantiate(UpgradePrefabs[Random.Range(0 , UpgradePrefabs.Length)]);

        upgrade.transform.position = position;
    }

    public float GameStagePercent()
    {
        return Mathf.Clamp01( (float)GameStage / MaxGameStage);
    }

}
