using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviourScript : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }
   
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {

            Destroy(gameObject);
    }

   
}
