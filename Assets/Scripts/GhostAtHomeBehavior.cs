using UnityEngine;
using System.Collections;
public class GhostAtHomeBehavior : GhostBehavior
{
    public Transform inside;

    public Transform outside;

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ghost.movement.SetDirection(-ghost.movement.direction);
        }
    }

    
    private IEnumerator ExitTransition()
    {
        ghost.movement.SetDirection(Vector2.up, true);
        ghost.movement.rigidb.isKinematic = true;
        ghost.movement.enabled = false;

        Vector3 position = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, inside.position, elapsed / duration);
            newPosition.z = position.z;
            ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(inside.position, outside.position, elapsed / duration);
            newPosition.z = position.z;
            ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        ghost.movement.rigidb.isKinematic = false;
        ghost.movement.enabled = true;
    }
}
