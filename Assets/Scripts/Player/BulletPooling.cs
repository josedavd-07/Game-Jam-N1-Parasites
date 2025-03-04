using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public int poolSize = 10; // Valor por defecto
    public GameObject bulletPrefab;
    private List<GameObject> bulletsCharged = new List<GameObject>();

    [SerializeField] private Transform shootPoint;
    private GameObject bulletSelected;

    void Start()
    {
        if (bulletsCharged == null)
            bulletsCharged = new List<GameObject>();

        MakePool(poolSize);

        if (bulletsCharged.Count == 0)
        {
            Debug.LogError("ERROR: No se han agregado balas a la lista bulletsCharged.");
        }
        else
        {
            Debug.Log($"Se han agregado {bulletsCharged.Count} balas a la lista.");
        }
    }

    public void MakePool(int size)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("ERROR: No se ha asignado un prefab de bala en BulletPooling.");
            return;
        }

        for (int i = 0; i < size; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletsCharged.Add(bullet);
        }

        Debug.Log($"Se crearon {bulletsCharged.Count} balas en el pool.");
    }

    public void FireBullets()
    {
        if (shootPoint == null)
        {
            Debug.LogError("ERROR: shootPoint no ha sido asignado en BulletPooling.");
            return;
        }

        bulletSelected = UseBullet();

        if (bulletSelected == null)
        {
            Debug.LogError("ERROR: No se pudo seleccionar una bala.");
            return;
        }

        bulletSelected.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
        bulletSelected.SetActive(true);
    }

    private GameObject UseBullet()
    {
        for (int i = 0; i < bulletsCharged.Count; i++)
        {
            if (bulletsCharged[i] == null)
            {
                // Si una bala fue destruida accidentalmente, la reemplazamos
                bulletsCharged[i] = Instantiate(bulletPrefab);
                bulletsCharged[i].SetActive(false);
            }

            if (!bulletsCharged[i].activeInHierarchy)
            {
                return bulletsCharged[i];
            }
        }

        // Si no hay balas disponibles, creamos una nueva
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        bulletsCharged.Add(newBullet);

        return newBullet;
    }
}
