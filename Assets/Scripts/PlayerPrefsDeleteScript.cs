using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsDeleteScript : MonoBehaviour
{
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs sıfırlandı.");
    }
}
