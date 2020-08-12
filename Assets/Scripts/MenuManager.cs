using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Text maximaPuntuacionText;

    private void Start()
    {
        int maximaPuntuacion = PlayerPrefs.GetInt("Puntuacion", 0);
        string maximaPuntuacionString = "Maxima puntuación \n " + maximaPuntuacion;
        maximaPuntuacionText.text = maximaPuntuacionString;

    }


    public void Jugar()
    {
        SceneManager.LoadScene("Juego");
    }


    public void Salir()
    {
        Application.Quit();
    }
}
