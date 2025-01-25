using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText1; //Score ตอนเล่น
    public TextMeshProUGUI scoreText2; //Score ตอนจบเกม

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText1.text = "Score: " + score.ToString();
        scoreText2.text = " " + score.ToString(); 
    }
}