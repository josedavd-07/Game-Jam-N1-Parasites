using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController2 : MonoBehaviour
{
    private int activeBullets = 0; // Contador de balas activas
    private const int maxBullets = 21; // Límite de balas simultáneas

    public Bullet bulletAntibiotic;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && activeBullets < maxBullets) // Verifica el límite antes de disparar
        {
            if (bulletAntibiotic != null) // Verifica si el prefab está asignado
            {
                Bullet db = Instantiate(bulletAntibiotic, this.transform.position, Quaternion.identity);

                if (db != null)
                {
                    db.destroyed += bulletDestroyed; // Suscribirse al evento de destrucción de la bala
                    activeBullets++; // Incrementa el contador de balas activas
                }
            }
            else
            {
                Debug.LogError("Bullet prefab not assigned!");
            }
        }
    }

    private void bulletDestroyed()
    {
        activeBullets--; // Reduce el contador cuando una bala se destruye
        if (activeBullets < 0) activeBullets = 0; // Previene valores negativos en caso de errores
    }
}



