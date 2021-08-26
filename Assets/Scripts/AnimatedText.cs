using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedText : MonoBehaviour
{
    public Text text { get; private set; }

    public Color[] colors;

    public float animationTime = 0.25f;

    public int animationFrame { get; private set; }

    public bool loop = true;
    private void Awake()
    {
        text= GetComponent<Text>();

    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), animationTime, animationTime);
    }

    private void Advance()
    {
        if (!text.enabled)
        {
            return;
        }

        animationFrame++;

        if (animationFrame >= colors.Length && loop)
        {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < colors.Length)
        {
            text.color = colors[animationFrame];
        }
    }

    public void Restart()
    {
        animationFrame = -1;

        Advance();
    }
}
