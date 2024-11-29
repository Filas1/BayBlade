using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class ScoreManager : MonoBehaviour
{
    public Label scoreText; 
    public Label highScoreText; 

    private int currentScore = 0;
    private int highScore = 0;

    private const string HighScoreKey = "HighScore";

    void Start()
    {
        // Načtení highscore z PlayerPrefs
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        UpdateUI();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(HighScoreKey, highScore); // Uloží highscore
        }
        UpdateUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score: {currentScore}";
        highScoreText.text = $"Highscore: {highScore}";
    }
}