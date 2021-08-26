using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public SpriteRenderer renderer { get; private set; }

    public Movement movement { get; private set; }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if(movement.direction == Vector2.up)
        {
            renderer.sprite = up;
        }
        else if (movement.direction == Vector2.down)
        {
            renderer.sprite = down;
        }
        else if (movement.direction == Vector2.left)
        {
            renderer.sprite = left;
        }
        else if (movement.direction == Vector2.right)
        {
            renderer.sprite = right;
        }
        
    }
}
