using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEvents : MonoBehaviour
{
    List<IInteractable> interactables = new List<IInteractable> ();

    private void OnTriggerEnter(Collider other)
    {
        IItem item;
        if (other.gameObject.TryGetComponent<IItem>(out item))
        {
            item.grab();
            return;
        }

        IInteractable interactable;
        if(other.gameObject.TryGetComponent<IInteractable>(out interactable))
        {
            interactables.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable;
        if (other.gameObject.TryGetComponent<IInteractable>(out interactable))
        {
            interactables.Remove(interactable);
        }
    }

    private void OnInteract(InputValue inputValue)
    {
        IInteractable nearest = null;
        float distance = Mathf.Infinity;
        foreach(IInteractable interactable in interactables)
        {
            if(nearest == null || distance > interactable.DistanceTo(transform.position))
            {
                nearest = interactable;
                distance = nearest.DistanceTo(transform.position);
            }
        }
        if(nearest != null)
        {
            nearest.Interact();
        }
    }
}
