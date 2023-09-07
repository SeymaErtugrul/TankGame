using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UITankAnimationScript : MonoBehaviour
{


    public GameObject cannon;

    private void Start()
    {
        StartCoroutine("shakeBodyAnim");
        StartCoroutine("shakeCannonAnim");
    }
    IEnumerator shakeBodyAnim()
    {
        while(true)
        {
            transform.DOShakePosition(0.3f,0.1f);
            yield return new WaitForSeconds(0.7f);
            

        }
    }

    IEnumerator shakeCannonAnim()
    {
        while(true)
        {
            cannon.transform.DOShakeRotation(3f, 5f, 1);
            yield return new WaitForSeconds(3f);
        }
    }
}
