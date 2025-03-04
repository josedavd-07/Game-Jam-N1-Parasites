using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    int maxLives = 3;
    int currentLives;
    public GameObject[] lifeIcons; 


    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;
        updateLifes();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Si presionas la barra espaciadora
        {
            Takedamage(); // Llama a la función de daño
            Debug.Log("Vida perdida. Vidas restantes: " + currentLives);
        }

        if (Input.GetKeyDown(KeyCode.R)) // Si presionas la tecla 'R'
        {
            ResetLifes(); // Reinicia las vidas
            Debug.Log("Vidas restauradas.");
        }
    }

    public void Takedamage()
    {
        currentLives--;
        updateLifes() ;
        if (currentLives == 0)
        {
            GameManager.GameInstance.GameOverLose();
            

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
