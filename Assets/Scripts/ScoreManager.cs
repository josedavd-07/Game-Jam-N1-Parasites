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
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Asegurar que haya solo una instancia
        }
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
