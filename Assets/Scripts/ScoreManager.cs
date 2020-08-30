using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class ScoreManager : MonoBehaviour
{

    public Text scoreGameplayText, scoreMultiplierText, coinsCollectedText,
            scoreFinalText, pointsFinalText ,coinsCollectedFinalText, lifesLeftFinalText
        ;

    public GameObject gameManager;

    private GameStateManager gsm;
    private int scorePerTick = 100;
    private int scoreMultiplierFactor = 1;

    private float nextTimeToScore;
    private int coinsCollected = 0;

    private int totalScore = 0;
    private bool isGameFinished = false;
    // Use this for initialization
    void Start()
    {

        nextTimeToScore = Time.timeSinceLevelLoad + 1;
        coinsCollectedText.text = coinsCollected.ToString();//starts at 0
        gsm = gameManager.GetComponent<GameStateManager>();

    }

    // Update is called once per frame
    void Update()
    {

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
        int totalPoints = GetTotalScore();
        int totalCoins = coinsCollected;
        int totalLifes = gsm.GetPlayerLifes();

        int totalScore = totalPoints;

        totalScore += (totalCoins * 10); //sum coins
        totalScore = totalLifes.Equals(0) ? totalScore : totalScore * totalLifes; //multiply lifes

        string txtScoreMessage = "Your score is \n " + totalScore;

        /*
         * pointsFinalText is the points text
         * coinsCollectedFinalText is the amount of coins text
         * lifesLeftFinalText the lifes that are left
         */
        pointsFinalText.text = "Points \n " + totalPoints.ToString();
        coinsCollectedFinalText.text = "Coin score \n " + totalCoins.ToString();
        lifesLeftFinalText.text = string.Format("Lifes Mult. \n {0}x", totalLifes.ToString());

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
        int incrementValue = scorePerTick * scoreMultiplierFactor;

        totalScore += incrementValue;
        scoreGameplayText.text = totalScore.ToString();
        scoreMultiplierText.text = "x" + scoreMultiplierFactor.ToString();
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
    public void ResetScoreMultiplier()
    {
        scoreMultiplierFactor = 0;
    }
}
