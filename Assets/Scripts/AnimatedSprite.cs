using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer sprRenderer { get; private set; }

    public Sprite[] sprites = new Sprite[0];

    public float animationTime = 0.25f;

    public int animationFrame { get; private set; }

    public bool loop = true;
    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), animationTime, animationTime);
    }

    private void Advance()
    {
        if (!sprRenderer.enabled)
        {
            return;
        }

        animationFrame++;

        if(animationFrame >= sprites.Length && loop)
        {
            animationFrame = 0;
        }

        if(animationFrame >=0 && animationFrame < sprites.Length)
        {
            sprRenderer.sprite = sprites[animationFrame];
        }
    }

    public void Restart()
    {
        animationFrame = -1;

        Advance();
    }
}
