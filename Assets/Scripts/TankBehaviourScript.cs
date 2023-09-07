using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehaviourScript : MonoBehaviour
{
    float speed = 4f;
    CharacterController controller;
    Vector3 moveDirection;
    public float rotateSpeed = 0.5f;
    public GameObject bullet;

    public Transform spawnPoint;
    public float moveDuration = 2.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical);
        transform.Rotate(0, Input.GetAxis("Horizontal") * 2, 0);
        controller.SimpleMove(moveDirection * speed);


        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        { 
                var ammo = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
