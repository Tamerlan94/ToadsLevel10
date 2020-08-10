using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextHome;

    public int delveScore = 0;
    public TextMeshProUGUI delveText;
   
    public static ScoreManager current;
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        scoreText.text = score.ToString("000000");
        delveText.text = delveScore.ToString("000000");

        scoreTextHome.text = score.ToString("000000");
    }
    public void AddScore(int a)
    {
        score += a;
        scoreText.text = score.ToString("000000");
        scoreTextHome.text = score.ToString("000000");
        SaveData.current.SaveDataScore();
        FindObjectOfType<ShopManager>().UpdateScore();
    }
    public void AddScoreDelve()
    {
        if (GameManager.current.isStart)
        {
            delveScore++;
            delveText.text = delveScore.ToString("000000");
        }        
    }
    
}
