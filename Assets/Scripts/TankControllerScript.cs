using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControllerScript : MonoBehaviour
{


    public float movementSpeed;
    Rigidbody rb;

    public float moveDuration = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 rotationAngles = new Vector3(0, 0, 0f);
            Quaternion rotation = Quaternion.Euler(rotationAngles);
            transform.rotation = rotation;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            rb.velocity = transform.forward * movementSpeed;
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 rotationAngles = new Vector3(0, 180, 0f);
            Quaternion rotation = Quaternion.Euler(rotationAngles);
            transform.rotation = rotation;
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 rotationAngles = new Vector3(0, 90, 0f);
            Quaternion rotation = Quaternion.Euler(rotationAngles);
            transform.rotation = rotation;
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 rotationAngles = new Vector3(0, 270, 0f);
            Quaternion rotation = Quaternion.Euler(rotationAngles);
            transform.rotation = rotation;
        }

        
    }
}
