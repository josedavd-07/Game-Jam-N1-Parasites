using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Bala impactó con: {other.gameObject.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Jugador impactado!");
            AudioManager.Instance.PlaySFX("PlayerDamaged");

            // Buscar el componente HealthManager
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.Takedamage(true); // Restar una vida
                Debug.Log("Vida restada.");
            }
            else
            {
                Debug.LogWarning("No se encontró HealthManager en el jugador.");
            }

            Destroy(gameObject); // Destruir la bala tras impactar
        }
    }


    private void OnDestroy()
    {
        if (destroyed != null)
        {
            destroyed.Invoke();
        }
    }
}
