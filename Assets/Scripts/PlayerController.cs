using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 10.0f;
    private float xRange = 20.0f;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        //obtenemos las entardas del usuario
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
         HealthManager health = other.gameObject.GetComponent<HealthManager>();
        if (other.gameObject.layer == LayerMask.NameToLayer("Gun")||other.gameObject.layer == LayerMask.NameToLayer("Virus") ) 
        {
            health.Takedamage(true);
            
        }
    }
}
