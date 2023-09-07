using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMovingScript : MonoBehaviour
{

    public float moveDistance = 2f;
    public float moveSpeed = 2f;
    public float delayBetweenMovements = 2f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = transform.position +moveDistance* Vector3.forward;
        MoveObject();
    }

    private void MoveObject()
    {
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition)
            {
                isMoving = false;
                Invoke("TeleportToInitialPosition", delayBetweenMovements);
            }
        }
    }

    private void TeleportToInitialPosition()
    {
        transform.position = initialPosition;
        Invoke("MoveObject", delayBetweenMovements);
    }
}
