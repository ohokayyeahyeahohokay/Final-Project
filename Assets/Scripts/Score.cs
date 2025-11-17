using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;

    void Start()
    {
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
        scoreText.text = "Score: " + score.ToString();
    }
}
