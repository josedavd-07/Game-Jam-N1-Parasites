using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    private void Start()
    {
        // Asegurar que la bala tiene un Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0; // Evita que la bala caiga por gravedad
        rb.velocity = direction * speed;
    }

    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Bala impactó con: {other.gameObject.name}");

        if (other.CompareTag("Player")) // Verifica que el objeto impactado es el jugador
        {
            Debug.Log("¡Jugador impactado!");

            // Buscar el componente HealthManager
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.Takedamage(true);
                Debug.Log("Vida restada.");
            }
            else
            {
                Debug.LogWarning("No se encontró HealthManager en el jugador.");
            }

            Destroy(gameObject); // Destruir la bala tras impactar
        }
    }
}
