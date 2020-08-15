using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts;

public class MenuManager : MonoBehaviour {

    public Text maxScoreText;

    private void Start()
    {
        int maxScore = PlayerPrefs.GetInt(Constants.Prefs.score, 0);
        string maxScoreString = "Max Score \n " + maxScore;
        maxScoreText.text = maxScoreString;
    }

    public void Play()
    {
        SceneManager.LoadScene(Constants.Scenes.game);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
