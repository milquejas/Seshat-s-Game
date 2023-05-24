using UnityEngine;

// Trigger collider toggles interactable bool
// set object with collider to interactables layer to not trigger player triggers?

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerInteractRange : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<IInteractable>(out IInteractable interactableScript))
        {
            interactableScript.InRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent<IInteractable>(out IInteractable interactableScript))
        {
            interactableScript.InRange = false;
        }
    }
}
