using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreGameplayText, scoreFinalText; 

    private int scorePerTick = 100;

    private float nextTimeToScore;

    private int totalScore = 0;
    private bool isGameFinished = false;
    // Use this for initialization
    void Start () {
        
        nextTimeToScore = Time.timeSinceLevelLoad + 1;

    }
	
	// Update is called once per frame
	void Update () {

        if (!isGameFinished)
        {
            if (Time.timeSinceLevelLoad >= nextTimeToScore)
            {
                IncreaseScore();
                nextTimeToScore = Time.timeSinceLevelLoad + 1;
            }
        }
    }

    public void EndGame()
    {
        isGameFinished = true;

        int maxScore = PlayerPrefs.GetInt("Score", 0);
        int totalScore = GetTotalScore();
        string txtScoreMessage = "Your score is \n " + totalScore;
     


        if (maxScore < totalScore)
        {
            txtScoreMessage += "\n New highscore!";
            PlayerPrefs.SetInt("Score", totalScore);
            PlayerPrefs.Save();
        }

        scoreFinalText.text = txtScoreMessage;
    }


    void IncreaseScore()
    {
        totalScore += scorePerTick;
        scoreGameplayText.text = totalScore.ToString();
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}
