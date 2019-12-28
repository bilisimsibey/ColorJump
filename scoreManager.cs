using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreManager : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI best;
    int currentScore = 0;

    private void Start()
    {
        GetBestScore();
        
    }
    void GetBestScore()
    {
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
    }

    public void addScore(int score)
    {
        currentScore += score;
        currentScoreText.text = currentScore.ToString();

        if (currentScore>PlayerPrefs.GetInt("BestScore",0))
        {
            bestScoreText.text = currentScore.ToString();
            PlayerPrefs.SetInt("BestScore",currentScore);
        }

    }
}
