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
                Debug.LogError("ERROR: No se encontr� ning�n objeto con BulletPooling en la escena.");
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !GameManager.GameInstance.isPaused) // "Fire1" es el bot�n universal para disparar
        {
            if (bulletPool != null)
            {
                bulletPool.FireBullets();
                
            }
            else
            {
                Debug.LogError("ERROR: bulletPool no est� asignado en ShipController.");
            }
        }
    }
}
