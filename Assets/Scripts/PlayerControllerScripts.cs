using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScripts : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.7f;
    private float gravityValue = -9.81f;
    private PlayerInput playerInput;

    private Transform cameraTransform;

    [SerializeField]
    private float rotationSpeed = 5f;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();

        Vector3 move = new Vector3(input.x,0,input.y);

        move= move.x * cameraTransform.right +move.z * cameraTransform.up;


        controller.Move(move * Time.deltaTime * playerSpeed);
        move.y = 0f;
        controller.Move(move*Time.deltaTime * playerSpeed);

 
        playerVelocity.y += gravityValue * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);

        if (input!=Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation=Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime*rotationSpeed);  
        }

        Debug.DrawRay(transform.position, move.normalized * 6f, Color.green); // move vektörünün yönünde 2 birimlik yeşil bir çizgi çiz
    }
}
