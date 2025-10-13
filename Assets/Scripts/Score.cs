using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;

    public int score = 0;
    public TextMeshProUGUI scoreText; // Optional if you have on-screen UI

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Total Score: " + score);

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
