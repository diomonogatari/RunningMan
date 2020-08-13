using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameStateManager : MonoBehaviour {

    public GameObject gameplayUI, endUI;

    public PlayerCharacter mainCharacter;
    public ScoreManager scoreManager;

    private bool isGameFinished = false;

	void Update () {
        if (!isGameFinished)
        {
            if (mainCharacter.IsCharacterDead()) EndTheGame();
        }
    }

    void EndTheGame()
    {
        isGameFinished = true;
        mainCharacter.StopCharacter();
    
        scoreManager.EndGame();

        StartCoroutine(DelaySceneTransition());
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