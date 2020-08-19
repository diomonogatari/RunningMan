using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class ScoreManager : MonoBehaviour {

    public Text scoreGameplayText, scoreFinalText, scoreMultiplierText, coinsCollectedText; 

    private int scorePerTick = 100;
    private int scoreMultiplierFactor = 1;

    private float nextTimeToScore;
    private int coinsCollected = 0;

    private int totalScore = 0;
    private bool isGameFinished = false;
    // Use this for initialization
    void Start () {
        
        nextTimeToScore = Time.timeSinceLevelLoad + 1;
        coinsCollectedText.text = coinsCollected.ToString();//starts at 0

    }
	
	// Update is called once per frame
	void Update () {

        if (!isGameFinished)
        {
            if (Time.timeSinceLevelLoad >= nextTimeToScore)
            {
                IncreaseScore();
                nextTimeToScore = Time.timeSinceLevelLoad + 1;
                scoreMultiplierFactor++;
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
            PlayerPrefs.SetInt(Constants.Prefs.score, totalScore);
            PlayerPrefs.Save();
        }

        scoreFinalText.text = txtScoreMessage;
    }


    void IncreaseScore()
    {
        int incrementValue = scorePerTick*scoreMultiplierFactor;

        totalScore += incrementValue;
        scoreGameplayText.text = totalScore.ToString();
        scoreMultiplierText.text = "x"+scoreMultiplierFactor.ToString();
    }
    public void IncreaseCoin()
    {
        coinsCollected++;
        coinsCollectedText.text = coinsCollected.ToString();
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}
