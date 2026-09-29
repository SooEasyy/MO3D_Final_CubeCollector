using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float timeLimit = 120f;

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;

    private float remainingTime;
    private bool timerActive = true;

    private void Start()
    {
        remainingTime = timeLimit;

        UpdateTimerUI();
    }

    private void Update()
    {
        if (!timerActive)
            return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;

            UpdateTimerUI();

            timerActive = false;

            GameManager.playerWon = false;
            GameManager.finalScore = GameManager.Instance.GetScore();

            SceneManager.LoadScene("GameOver");

            return;
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = string.Format(
            "{0:00}:{1:00}",
            minutes,
            seconds
        );
    }
}