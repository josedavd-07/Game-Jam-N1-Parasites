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


    //public void Takedamage(bool cur)
    //{
    //    currentLives--;
    //    updateLifes() ;
    //    if (currentLives == 0)
    //    {
    //        GameManager.GameInstance.GameOverLose();


    //    }
    //}


    public void Takedamage(bool cur)
    {
        currentLives--;
        updateLifes();
        Debug.Log("Vida perdida. Vidas restantes: " + currentLives);

        if (currentLives > 0) // Generar power-up solo si quedan vidas
        {
            if (powerUpManager != null)
            {
                Debug.Log("Llamando a SpawnPowerUp()...");
                powerUpManager.SpawnPowerUp();
            }
            else
            {
                Debug.LogWarning("Error: powerUpManager no está asignado en HealthManager.");
            }
        }

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
