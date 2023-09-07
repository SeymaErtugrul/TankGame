using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandlerScript : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject auidoPanel;
    public GameObject controlsPanel;

    public AudioSource hoverSound;
    public AudioSource pressedSound;

    public bool auidoClick = false;

    public Animator animator;
    public Animation animation;
    bool animationComplete = false;
    public void setActiveSetting()
    {

        StartCoroutine("wait");

    }

    public void setActiveSettingsBack()
    {
        StartCoroutine("wait");

    }


    public void setActiveAudioPanel()
    {

        StartCoroutine("waitUI");
    }

    public void laodScene()
    {
        StartCoroutine("waitUI");
    }



    public void setActiveMenuPanelBack()
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
    public void setActiveControlsPanel()
    {
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }


    public void playHoverAudio()
    {
        hoverSound.Play();
    }

    public void playPressSound()
    { pressedSound.Play(); }

  public void backToGame()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void playAnim()
    {
        animator.Play("tankShoot");
        StartCoroutine(loadSceneAfterAnimation());
    }

    public void playAnimParticle()
    {
       
        animation.Play();
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.25f);

        if (auidoPanel.activeSelf)
        {
            auidoPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
      
          
        if (controlsPanel.activeSelf)
        {
            controlsPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }

        if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);  //menuden ayarlara
            settingsPanel.SetActive(true);
        }

       
    }

    

    private IEnumerator loadSceneAfterAnimation()
    {
        yield return new WaitForSeconds(1.1f);

        SceneManager.LoadScene(1); 
    }
    IEnumerator waitUI()
    {
        yield return new WaitForSeconds(0.25f);

        if (menuPanel.activeSelf && animationComplete)
        {
          
            SceneManager.LoadScene(1);
        }

        if (settingsPanel.activeSelf)
        { 
            settingsPanel.SetActive(false);
            auidoPanel.SetActive(true);
        }



        if (settingsPanel.activeSelf && auidoClick)
        {
            Debug.Log("tıklandı");
            settingsPanel.SetActive(false);
            auidoPanel.SetActive(true);
            auidoClick = false;
        }




        if (settingsPanel.activeSelf && auidoClick)
        {
            Debug.Log("tıklandı");
            settingsPanel.SetActive(false);
            auidoPanel.SetActive(true);
            auidoClick = false;
        }

       

    }

    public void auidoClickTrue()
    {
        auidoClick = true;
  
    }

}
