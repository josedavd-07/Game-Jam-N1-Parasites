using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}

public class GameManager : MonoBehaviour
{
    public static GameManager GameInstance { get; private set; }
    public bool isPaused;

    public DifficultyLevel currentDifficult;

    public event Action OnWin;
    public event Action OnLose;

    private void Awake()
    {
        if (GameInstance == null)
        {
            GameInstance = this;
            DontDestroyOnLoad(this.gameObject);

            currentDifficult = (DifficultyLevel)PlayerPrefs.GetInt("Difficulty", (int)DifficultyLevel.Easy);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void GameOverWin()
    {
        Debug.Log("You win!");
        isPaused = true;
        Time.timeScale = 0;
        OnWin?.Invoke();
    }

    public void GameOverLose()
    {
        Debug.Log("You lose!");
        isPaused = true;
        Time.timeScale = 0;
        OnLose?.Invoke();
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        isPaused = false;

        AudioManager.Instance.musicSource.Stop();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        AudioManager.Instance.PlayMusic("Theme");
    }

    public void SetDifficulty(DifficultyLevel newDifficulty)
    {
        currentDifficult = newDifficulty;
        PlayerPrefs.SetInt("Difficulty", (int)newDifficulty); // Guarda la dificultad
        PlayerPrefs.Save();
    }
}
