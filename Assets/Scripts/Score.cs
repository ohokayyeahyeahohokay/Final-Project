using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Total Score: " + score);

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
