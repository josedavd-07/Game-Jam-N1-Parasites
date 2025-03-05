using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupadd : MonoBehaviour
{
    public float lifeTime = 5f; // Tiempo antes de desaparecer

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Se autodestruye si nadie lo recoge
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 🔹 Asegúrate de que el Player tiene la etiqueta "Player"
        {
            HealthManager healthManager = other.GetComponent<HealthManager>();

            if (healthManager != null)
            {
                healthManager.AddLife(); // 🔹 Sumar vida al jugador
                Debug.Log("🔹 Vida extra obtenida!");
                Destroy(gameObject); // 🔹 Elimina el Power-Up tras recogerlo
            }
        }
    }
}
