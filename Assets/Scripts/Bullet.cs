using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si destroyed no es null antes de invocarlo
        if (destroyed != null)
        {
            destroyed.Invoke();
        }
        
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (destroyed != null)
        {
            destroyed.Invoke();
        }
    }
}
