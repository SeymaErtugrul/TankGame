using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class ButtonOutlineScript : MonoBehaviour
{
    private Vector3 originalScale;
    private Outline outline;
    private bool isHovering = false;

    private Sequence glowSequence;
    private Image buttonImage;
    private void Start()
    {
        outline = GetComponent<Outline>();
        originalScale = transform.localScale;
    }

    public void  buttonhoverScaleMax()
    {
            transform.DOScale(originalScale * 1.2f, 0.2f);
    }
    public void buttonhoverScaleMin()
    {
        transform.DOScale(originalScale, 0.2f);
    }

    public void glowSequenceRestart()
    {

        glowSequence.Restart();
    }

    public void glowSequenceUpdate()
    {

        glowSequence.Rewind();
    }
}
