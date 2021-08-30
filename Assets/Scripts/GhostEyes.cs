using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public SpriteRenderer sprRenderer { get; private set; }

    public Movement movement { get; private set; }

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if(movement.direction == Vector2.up)
        {
            sprRenderer.sprite = up;
        }
        else if (movement.direction == Vector2.down)
        {
            sprRenderer.sprite = down;
        }
        else if (movement.direction == Vector2.left)
        {
            sprRenderer.sprite = left;
        }
        else if (movement.direction == Vector2.right)
        {
            sprRenderer.sprite = right;
        }
        
    }
}
