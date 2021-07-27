using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speedMultiplayer = 1.0f;

    public float speed = 8.0f;

    public Vector2 initialDirection;

    public Vector2 direction { get; private set; }

    public Vector2 nextDirection { get; private set; }

    public Vector3 startingPosition { get; private set; }

    public LayerMask obstacleLayer;
    public Rigidbody2D rigidb { get; private set; }

    private void Awake()
    {
        rigidb = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        speedMultiplayer = 1.0f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        rigidb.isKinematic = false;
        this.enabled = true;
    }

    private void Update()
    {
        if(nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidb.position;
        Vector2 translation = direction * speed * speedMultiplayer * Time.fixedDeltaTime;
        rigidb.MovePosition(position + translation);
    }

    public void SetDirection (Vector2 direction, bool forced = false)
    {
        if(!Occupied(direction) || forced)
        {
            this.direction = direction;
            nextDirection = Vector2.one;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }
}
