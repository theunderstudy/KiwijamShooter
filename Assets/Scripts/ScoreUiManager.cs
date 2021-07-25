using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUiManager : MonoBehaviour
{
    [SerializeField] Text killsCount;
    [SerializeField] Text survivedDuration;
    [SerializeField] Text score;

    private void Start()
    {
        killsCount.text = GameManager.instance.kills.ToString();
        survivedDuration.text = GameManager.instance.survivalDur.ToString();
        score.text = GameManager.instance.score.ToString();
    }
}
