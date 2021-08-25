using UnityEngine;

public class ScatteredGhostBehavior : GhostBehavior
{
    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            ghost.chase.Enable();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if(node!= null && this.enabled && ! ghost.scared.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if(node.availableDirections[index] == -ghost.movement.direction && node.availableDirections.Count >1)
            {
                index++;

                if(index >= node.availableDirections.Count)
                {
                    index -= 2;
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
