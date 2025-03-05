using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D bulletRb; // Asegurar que est� asignado en el prefab
    public float bulletSpeed = 500f; // Ajustar seg�n necesidad
    public float lifetime = 3f;

    private bool firstEnable = true;

    void OnEnable()
    {
        if (firstEnable)
        {
            firstEnable = false;
            return;
        }

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

        AudioManager.Instance.PlaySFX("PlayerShoot");

        // Aplicar la fuerza en la direcci�n correcta
        bulletRb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);

        StartCoroutine(DisableAfterTime(lifetime)); // Usar una corrutina en vez de Invoke
    }

    IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false); // Desactivar la bala correctamente
    }
}
