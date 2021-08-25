using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int points;

    public Movement movement { get; private set; }
    public GhostAtHomeBehavior home { get; private set; }
    public ScaredGhostBehavior scared { get; private set; }
    public ChasingGhostBehavior chase { get; private set; }
    public ScatteredGhostBehavior scatter { get; private set; }

    public GhostBehavior initialBehavior;

    public Transform target;
    private void Awake()
    {
        points = 200;
        movement = GetComponent<Movement>();
        home = GetComponent<GhostAtHomeBehavior>();
        scared = GetComponent<ScaredGhostBehavior>();
        scatter = GetComponent<ScatteredGhostBehavior>();
        chase = GetComponent<ChasingGhostBehavior>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        movement.ResetState();

        scared.Disable();
        chase.Disable();
        scatter.Enable();

        if(home!= initialBehavior)
        {
            home.Disable();
        }

        if(initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.scared.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
