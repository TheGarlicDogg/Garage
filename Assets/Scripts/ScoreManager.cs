using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int winScore = 9;
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    int score = 0;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = "Положите вещи в кузов пикапа (" + score.ToString() + "/9)";
    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = "Положите вещи в кузов пикапа (" + score.ToString() + "/9)";
        if (score == winScore)
        {
            scoreText.text = "Вы победили!";
        }
    }
    public void DecreaseScore()
    {
        score -= 1;
        scoreText.text = "Положите вещи в кузов пикапа (" + score.ToString() + "/9)";

    }
}
