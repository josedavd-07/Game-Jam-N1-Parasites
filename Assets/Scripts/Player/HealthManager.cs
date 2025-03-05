using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //Cambios hecho por jose
    public PowerUp powerUpManager; // Referencia al PowerUp

    int maxLives = 3;
    int currentLives;
    public GameObject[] lifeIcons; 


    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;
        updateLifes();
    }


    public void Takedamage(bool cur)
    {
        currentLives--;
        updateLifes() ;
        if (currentLives == 0)
        {
            GameManager.GameInstance.GameOverLose();
            AudioManager.Instance.PlaySFX("PlayerExplosion");
            AudioManager.Instance.PlayMusic("DeadTheme");
        }
    }

    public void AddLife()
    {
        if (currentLives < maxLives) // No puede superar el máximo
        {
            currentLives++;
            updateLifes();
            Debug.Log("❤ Vida extra obtenida! Vidas actuales: " + currentLives);
        }
    }


    void updateLifes()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].SetActive(i < currentLives);
        }
    }

    public void ResetLifes()
    {
        currentLives =maxLives;
        updateLifes();
    }
}
