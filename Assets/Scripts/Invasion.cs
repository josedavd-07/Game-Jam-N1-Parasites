using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

using UnityEngine;

using UnityEngine;

public class Invasion : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows = 3;
    public int columns = 9;
    
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
            float width = 1.4f * (this.columns - 1);
            float height = 1.4f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 1.4f), 0.0f);

            for (int col = 0; col < this.columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row % this.prefabs.Length], this.transform);
                Vector3 position = rowPosition;
                position.x += col * 1.4f;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Update()
    {
        this.transform.position += direction * this.speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (direction == Vector3.right && invader.position.x >= (rightEdge.x - 0.5f))
            {
                RowDown();
                break;
            }
            else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 0.5f))
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
}



