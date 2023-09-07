using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OptionsMenuScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource uıAudio;
    public Slider volumeSlider;
    public float volumeValue;
    public void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }


    public void setVolume(float volume)
    {
        volumeValue = volume; 
    }

    private void Update()
    {
        audioMixer.SetFloat("volume", volumeValue);
        uıAudio.volume = volumeValue;
        PlayerPrefs.SetFloat("volume", volumeValue); 
    }

}
