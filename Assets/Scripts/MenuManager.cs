using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Text maxScoreText;

    private void Start()
    {
        int maxScore = PlayerPrefs.GetInt("Score", 0);
        string maxScoreString = "Max Score \n " + maxScore;
        maxScoreText.text = maxScoreString;
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
