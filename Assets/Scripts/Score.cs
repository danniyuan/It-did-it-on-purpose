using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int TotalScore = 0;
    public Text ScoreBar;
    public Text FinalScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreBar.text = ("Current Score: " + TotalScore);
        FinalScore.text = ("Your Final Score is " + TotalScore);
    }
}
