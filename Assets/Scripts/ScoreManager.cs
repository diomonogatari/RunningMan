using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text puntuacionGameplayText, puntuacionFinalText; 

    private int scorePerTick = 100;

    private float nextTimeToScore;

    private int totalScore = 0;
    private bool juegoAcabado = false;
    // Use this for initialization
    void Start () {
        //Empezamos teniendo el cuenta en tiempo para que el score no empiece ya en 100
        nextTimeToScore = Time.timeSinceLevelLoad + 1;

    }
	
	// Update is called once per frame
	void Update () {

        if (!juegoAcabado)
        {
            if (Time.timeSinceLevelLoad >= nextTimeToScore)
            {
                AumentarScore();
                nextTimeToScore = Time.timeSinceLevelLoad + 1;
            }
        }
    }

    public void AcabarJuego()
    {
        juegoAcabado = true;

        int maximaPuntuacion = PlayerPrefs.GetInt("Puntuacion", 0);
        int puntuacionTotal = GetTotalScore();
        string puntuacionString = "Tu puntuación ha sido de \n " + puntuacionTotal;
     


        if (maximaPuntuacion < puntuacionTotal)
        {
            puntuacionString += "\n ¡Nueva puntuación maxima alcanzada!";
            PlayerPrefs.SetInt("Puntuacion", puntuacionTotal);
            PlayerPrefs.Save();
        }

        puntuacionFinalText.text = puntuacionString;
    }


    void AumentarScore()
    {
        totalScore += scorePerTick;
        puntuacionGameplayText.text = totalScore.ToString();
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}
