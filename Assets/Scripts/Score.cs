using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Initialize the score display when the game starts
        score = 0;
        UpdateScoreDisplay();
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Total Score: " + score);
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }
}
