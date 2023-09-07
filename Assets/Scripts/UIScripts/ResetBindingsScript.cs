using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ResetBindingsScript : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

   
    void Start()
    {
        resetBindings();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void resetBindings()
    {
        foreach(InputActionMap map in inputActions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
    }
}
