using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUiManager : MonoBehaviour
{
    [SerializeField] Text killsCount;
    [SerializeField] Text survivedDuration;
    [SerializeField] Text score;

    private void OnEnable()
    {
        killsCount.text = string.Empty;
        survivedDuration.text = string.Empty;
        score.text = string.Empty;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
