using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum EnemyType { nill, bomber, pistol }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TopDownCharacterController player;
    public int GameStage = 1;
    public int MaxGameStage = 10;

    public Upgrade[] UpgradePrefabs;
    public int kills = 0;
    public int killsValue = 0;
    public float survivalDur = 1;
    public float survivalScorePerSecond = 1;
    public int score;
    private bool playeralive = true;
    public ScoreUiManager scoreScreen;
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
        scoreScreen = FindObjectOfType<ScoreUiManager>();

    }
    private void Update()
    {
        if (playeralive)
            survivalDur += Time.deltaTime;
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

    public void EnemyDied(EnemyType _t)
    {
        kills++;
        switch (_t)
        {
            case EnemyType.bomber:
                killsValue += 1;
                break;
            case EnemyType.pistol:
                killsValue += 1;
                break;
            case EnemyType.nill:
                Debug.LogError("You need to set the enemy type on the enemy who just got killed");
                break;
        }
    }

    public void GameOver()
    {
        CalculateScore();
        //player sprite should already be a patch of blood particles
        // screenshake
        // after a second or 3 show score screen
        scoreScreen.gameObject.SetActive(true);
        // screenshake with visibility on of survived for dur
        // screenshake with visibility on of kills

    }


    private void CalculateScore()
    {
        int killScore = kills * killsValue;
        int survivalScore = (int)(survivalDur / survivalScorePerSecond);
        score = killScore + survivalScore;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }


    //Enemy vision - do a raycast back to the player and only move towards if can see
}
