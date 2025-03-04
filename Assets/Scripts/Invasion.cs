using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invasion : MonoBehaviour
{
    public Virus[] prefabs;
    public int rows = 3;
    public int columns = 9;
    public int totalKO { get; private set; }
    public float percentKO => (float)this.totalKO / ((float)this.rows * this.columns);
    
    private float speed = 1.0f; // Velocidad inicial reducida para un inicio más balanceado
    private float maxSpeed = 8.0f; // Velocidad máxima que puede alcanzar
    private float speedIncreaseFactor = 1.1f; // Factor de aumento de velocidad

    private float dropDistance = 1.0f; // Distancia inicial de bajada
    private float minDropDistance = 0.3f; // Distancia mínima de bajada (para evitar descensos bruscos)

    private Vector3 direction = Vector2.right;

    private void Awake()
    {
        for (int row = 0; row < this.rows; row++)
        {
            float width = 5f * (this.columns - 1);
            float height = 5f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 5f), 0.0f);

            for (int col = 0; col < this.columns; col++)
            {
                Virus virus = Instantiate(this.prefabs[row % this.prefabs.Length], this.transform);
                virus.death += VirusDeath; // Corregido: ahora usa una instancia
                Vector3 position = rowPosition;
                position.x += col * 5f;
                virus.transform.localPosition = position;
            }
        }
    }

    private void Update()
    {
        this.transform.position += direction * this.speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform virus in this.transform)
        {
            if (!virus.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (direction == Vector3.right && virus.position.x >= (rightEdge.x - 1.5f))
            {
                RowDown();
                break;
            }
            else if (direction == Vector3.left && virus.position.x <= (leftEdge.x + 1.5f))
            {
                RowDown();
                break;
            }
        }
    }

    private void RowDown()
    {
        direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        
        // Asegurar que la bajada no sea menor que el mínimo
        position.y -= Mathf.Max(dropDistance, minDropDistance);
        this.transform.position = position;

        // Aumentar la velocidad progresivamente hasta un máximo
        speed = Mathf.Min(speed * speedIncreaseFactor, maxSpeed);
    }

    private void VirusDeath()
    {
        this.totalKO++;

        // Asegurarte de que ScoreManager existe antes de llamarlo
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(10); // Sumar 10 puntos por cada enemigo destruido
        }
    }
}





