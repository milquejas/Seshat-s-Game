using UnityEngine;

// Trigger collider toggles interactable bool

public class PlayerInteractRange : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<IInteractable>(out IInteractable interactableScript))
        {
            interactableScript.inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent<IInteractable>(out IInteractable interactableScript))
        {
            interactableScript.inRange = false;
        }
    }
}
