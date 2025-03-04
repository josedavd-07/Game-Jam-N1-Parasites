using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public BulletPooling bulletPool;

    void Start()
    {
        if (bulletPool == null)
        {
            bulletPool = FindObjectOfType<BulletPooling>();
            if (bulletPool == null)
            {
                Debug.LogError("ERROR: No se encontró ningún objeto con BulletPooling en la escena.");
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // "Fire1" es el botón universal para disparar
        {
            if (bulletPool != null)
            {
                bulletPool.FireBullets();
            }
            else
            {
                Debug.LogError("ERROR: bulletPool no está asignado en ShipController.");
            }
        }
    }
}
