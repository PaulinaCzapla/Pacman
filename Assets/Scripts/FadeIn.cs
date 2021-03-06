using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// TODO: REFACTOR
public class FadeIn : MonoBehaviour
{
    public CanvasGroup canvasGroup { get; private set; }

    public float animationTime = 0.25f;
    public int animationFrame { get; private set; }

    private float alpha = 0;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        animationFrame = 0;

    }

    private void Update() 
    {
        if (this.enabled == false)
        {
            animationFrame = 0;
            return;
        }
        else if (!canvasGroup.isActiveAndEnabled || animationFrame >= 100)
        {
            return;
        }
        else if (animationFrame < 100)
        {
            animationFrame++;
            alpha = animationFrame / 100f;
            canvasGroup.alpha = alpha;
        }
    }

    public void Restart()
    {
        animationFrame = -1;
        alpha = 0;
    }
}
