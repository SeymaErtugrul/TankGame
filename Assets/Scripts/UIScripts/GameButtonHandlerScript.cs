using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonHandlerScript : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject audioPanel;
    public GameObject controlPanel;
    
    public void settingsActive()
    {
        settingsPanel.SetActive(true);  
    }

    public void audioPanelSetActive()
    {
        settingsPanel.SetActive(false);
        audioPanel.SetActive(true);
    }

    public void settingsSetActiveBack()
    {
        if (controlPanel.activeSelf)
        {
            controlPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
        if (audioPanel.activeSelf)
        {
            audioPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
       
    }

 
    public void controlPanelActive()
    {
        settingsPanel.SetActive(false);
        controlPanel.SetActive(true);
    }
}

