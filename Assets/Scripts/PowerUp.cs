using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject prefabPowerUp; // Prefab del power-up
    public Transform spawnPoint; // Lugar donde aparecerá el power-up

    public void SpawnPowerUp()
    {
        if (prefabPowerUp != null && spawnPoint != null)
        {
            Instantiate(prefabPowerUp, spawnPoint.position, Quaternion.identity);
            Debug.Log("Power-up generado.");
        }
    }
}
