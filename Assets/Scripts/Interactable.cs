using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;
    Transform player;
    bool inRange;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        Debug.Log("Interact");
    }

    private void Update()
    {
        if (inRange && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }


    public void Close(Transform playerTransform)
    {
        inRange = true;
        player = playerTransform;
    }

    public void NotClose()
    {
        inRange = false;
        player = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
