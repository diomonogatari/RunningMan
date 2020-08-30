using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameStateManager : MonoBehaviour
{

    public GameObject gameplayUI, endUI, pauseUI;

    public PlayerCharacter mainCharacter;
    public ScoreManager scoreManager;

    public Text hpText;

    public static bool gameIsPaused;

    private bool isGameFinished = false;

    private void Start()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        Debug.Log("I was called");
    }

    void Update()
    {
        if (!isGameFinished)
        {
            if (mainCharacter.IsCharacterDead())
                EndTheGame(true);
        }
        hpText.text = mainCharacter.GetCurrentHp().ToString();
    }

    public void EndTheGame(bool stopTheCharacter)
    {
        isGameFinished = true;
        if (stopTheCharacter)
            mainCharacter.StopCharacter();

        scoreManager.EndGame();

        StartCoroutine(DelaySceneTransition());
    }
    public int GetPlayerLifes()
    {
        return mainCharacter.GetCurrentHp();
    }
    public void PauseGame()
    {
        if (!gameIsPaused)//if it isn't pause, it will pause
        {
            Time.timeScale = 0f;
            gameIsPaused = !gameIsPaused;
            pauseUI.SetActive(true);
        }
        else//else it is paused, then unpause
        {
            Time.timeScale = 1f;
            gameIsPaused = !gameIsPaused;
            pauseUI.SetActive(false);
        }
    }

    [ContextMenu("Restart the Game")]
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(Constants.Scenes.homeMenu);
    }

    IEnumerator DelaySceneTransition()
    {
        yield return new WaitForSeconds(2);

        gameplayUI.SetActive(false);
        endUI.SetActive(true);
    }
}