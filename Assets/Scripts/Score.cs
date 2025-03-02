using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText; // Cambiar GameObject por TextMeshProUGUI

    private float score = 0;

    void FixedUpdate()
    {
        score = score + 1 * (float)Time.deltaTime;
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + (int)score;
        }
    }
}