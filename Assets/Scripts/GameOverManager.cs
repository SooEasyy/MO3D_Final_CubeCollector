using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        if (GameManager.playerWon)
            titleText.text = "¡VICTORIA!";
        else
            titleText.text = "GAME OVER";

        scoreText.text = "Puntaje: " + GameManager.finalScore;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Level01");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}