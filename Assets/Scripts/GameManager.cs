using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Juego")]
    public int score = 0;
    public int lives = 3;
    public static bool playerWon;
    public static int finalScore;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image[] hearts;
    [SerializeField] private int pointsToWin = 10;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void UpdateUI()
    {
        scoreText.text = "x " + score;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < lives;
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();

        if (score >= pointsToWin)
        {
            playerWon = true;
            finalScore = score;

            SceneManager.LoadScene("GameOver");
        }
    }
    public void LoseLife()
    {
        lives--;

        UpdateUI();

        if (lives <= 0)
        {
            playerWon = false;
            finalScore = score;

            SceneManager.LoadScene("GameOver");
        }
    }
        public int GetScore()
    {
        return score;
    }
}