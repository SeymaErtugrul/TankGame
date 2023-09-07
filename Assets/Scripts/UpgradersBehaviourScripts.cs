using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradersBehaviourScripts : MonoBehaviour
{
    public float rotationSpeed = 15f; // Dönme hızı
   
    void Update()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

  

}
