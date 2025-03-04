using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D bulletRb; // Asegurar que esté asignado en el prefab
    public float bulletSpeed = 500f; // Ajustar según necesidad
    public float lifetime = 3f;

    void OnEnable()
    {
        if (bulletRb == null)
        {
            bulletRb = GetComponent<Rigidbody2D>();
            if (bulletRb == null)
            {
                Debug.LogError("ERROR: No se encontró Rigidbody2D en la bala.");
                return;
            }
        }

        bulletRb.velocity = Vector2.zero; // Resetear velocidad
        bulletRb.angularVelocity = 0f;    // Evitar rotaciones inesperadas

        // Aplicar la fuerza en la dirección correcta
        bulletRb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);

        StartCoroutine(DisableAfterTime(lifetime)); // Usar una corrutina en vez de Invoke
    }

    IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false); // Desactivar la bala correctamente
    }
}
