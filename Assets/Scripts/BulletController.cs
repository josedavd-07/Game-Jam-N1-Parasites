using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D bulletRb; // Asegurar que est� asignado en el prefab
    public float bulletSpeed = 500f; // Ajustar seg�n necesidad
    public float lifetime = 3f;

    void OnEnable()
    {
        if (bulletRb == null)
        {
            bulletRb = GetComponent<Rigidbody2D>();
            if (bulletRb == null)
            {
                Debug.LogError("ERROR: No se encontr� Rigidbody2D en la bala.");
                return;
            }
        }

        bulletRb.velocity = Vector2.zero; // Resetear velocidad
        bulletRb.angularVelocity = 0f;    // Evitar rotaciones inesperadas

        // Aplicar la fuerza en la direcci�n de la bala
        bulletRb.AddForce(transform.up * bulletSpeed);

        // Destruir despu�s de un tiempo
        Invoke("DisableBullet", lifetime);
    }

    void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}
