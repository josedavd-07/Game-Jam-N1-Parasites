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
    public int StillAlive => (int)(this.rows * this.columns) - this.totalKO;
    private float baseSpeed = 1.0f;
    private float speed;
    private float maxSpeed = 15.0f;
    private int waveNumber = 1;
    public int maxWaves = 5;
    //private float speedIncreaseFactor = 1.1f;
    private float dropDistance = 1.0f;
    private float minDropDistance = 0.3f;
    private Vector3 direction = Vector2.right;
    private Vector3 initialPosition;
    public float FrequencyAttack = 0.5f; // Frecuencia de ataque reducida para más disparos
    public Bullet bullet;
    private float attackCooldown = 5.0f; // Empieza con disparos poco frecuentes
    private float minAttackCooldown = 1.0f;

    private void Start()
    {
        initialPosition = transform.position;
        StartNewWave();
        InvokeRepeating(nameof(EnemyAttack), 1.0f, FrequencyAttack); // Inicia los ataques
    }

    private void StartNewWave()
    {
        if (waveNumber > maxWaves)
        {
            Debug.Log("¡Has completado todas las oleadas! Fin del juego.");
            GameManager.GameInstance.GameOverWin();
            return;
        }

        totalKO = 0;
        speed = Mathf.Min(baseSpeed * Mathf.Pow(2, waveNumber - 1), maxSpeed);

        // 🔹 Disminuir la frecuencia de ataque más rápido por cada ola
        FrequencyAttack = Mathf.Max(FrequencyAttack - 0.7f, 0.7f); // Nunca menos de 0.2s

        // 🔹 Reducir el cooldown de ataque en 0.5 por ola (puedes aumentarlo a 0.7 o 1.0 si quieres más agresividad)
        attackCooldown = Mathf.Max(5.0f - (waveNumber * 1f), minAttackCooldown); // Nunca menor a 1.0s

        Debug.Log($"Ola {waveNumber} - Nueva frecuencia de ataque: {FrequencyAttack}, Cooldown: {attackCooldown}");

        // Reiniciar la posición del grupo de enemigos
        transform.position = initialPosition;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        SpawnEnemies();

        // 🔹 Reiniciar los ataques con la nueva frecuencia
        CancelInvoke(nameof(EnemyAttack));
        InvokeRepeating(nameof(EnemyAttack), attackCooldown, FrequencyAttack);
    }


    private void SpawnEnemies()
    {
        for (int row = 0; row < rows; row++)
        {
            float width = 5f * (columns - 1);
            float height = 5f * (rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 5f), 0.0f);

            for (int col = 0; col < columns; col++)
            {
                Virus virus = Instantiate(prefabs[row % prefabs.Length], transform);
                virus.death += VirusDeath;
                Vector3 position = rowPosition;
                position.x += col * 5f;
                virus.transform.localPosition = position;
            }
        }
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform virus in transform)
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
        Vector3 position = transform.position;
        position.y -= Mathf.Max(dropDistance, minDropDistance);
        transform.position = position;
    }

    private void VirusDeath()
    {
        totalKO++;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(10);
        }

        if (totalKO >= (rows * columns))
        {
            Debug.Log($"¡Oleada {waveNumber} completada!");
            waveNumber++;
            StartNewWave();
        }
    }

    private void EnemyAttack()
    {
        Debug.Log($"Intentando disparar... Enemigos vivos: {StillAlive}");

        if (bullet == null)
        {
            Debug.LogError("Error: No se ha asignado un prefab de bullet en el Inspector.");
            return;
        }

        foreach (Transform virus in this.transform)
        {
            if (!virus.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (StillAlive > 0 && Random.value < 0.2f) // 20% de probabilidad de ataque
            {
                Instantiate(this.bullet, virus.position, Quaternion.identity);
                AudioManager.Instance.PlaySFX("MonsterShoot");
                Debug.Log($"Virus en {virus.position} ha disparado.");
                break;
            }
        }
    }
}
