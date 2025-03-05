using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int score;
    public TextMeshProUGUI scoreText;
   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        GameManager.GameInstance.OnWin += HandleGameWin;
        GameManager.GameInstance.OnLose += HandleGameOver;
    }

    private void HandleGameWin()
    {
        gameObject.SetActive(false); 
    }

    private void HandleGameOver()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.GameInstance.OnWin -= HandleGameWin;
        GameManager.GameInstance.OnLose -= HandleGameOver;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScore();
    }


    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: "+ score;
        }
    }
}
