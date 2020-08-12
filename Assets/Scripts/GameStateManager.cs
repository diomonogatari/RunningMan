using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {

    public GameObject gameplayUI, endUI;

    public Personaje personajePrincipal;
    public ScoreManager scoreManager;

    private bool juegoTerminado = false;

	void Update () {
        if (!juegoTerminado)
        {
            if (personajePrincipal.EstaMuerto()) AcabarJuego();

        }
    }


    void AcabarJuego()
    {
        juegoTerminado = true;
        personajePrincipal.DetenerPersonaje();
    
     
        scoreManager.AcabarJuego();


        StartCoroutine(DelayEnseñarInterfaz());
    }

    [ContextMenu("Reiniciar el juego")]
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    IEnumerator DelayEnseñarInterfaz()
    {
        yield return new WaitForSeconds(2);
     
        gameplayUI.SetActive(false);
        endUI.SetActive(true);
    }
}
