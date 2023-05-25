using System.Collections.Generic;
using UnityEngine;

/* 
 * check if interactable near touch location -> check if in player range -> interact
 * For trying to interact with objects in level
*/

public static class InteractSystem
{
    private static LayerMask interactableLayer = LayerMask.GetMask("Interactable");
    private static ContactFilter2D interactableContactFilter;

    private static List<Collider2D> interactables = new List<Collider2D>();
    private static Collider2D closestCollider;

    // constructor called once automatically
    static InteractSystem()
    {
        interactableContactFilter.SetLayerMask(interactableLayer);
    }

    public static bool TryToInteract(Vector2 touchPosition, float size)
    {
        int colliderAmountFound = Physics2D.OverlapCircle(touchPosition, size, interactableContactFilter, interactables);
        
        if (colliderAmountFound > 0)
        {
            closestCollider = interactables[0];

            // closest in list to position should be picked
            // OverlapCircle doesn't give position of collision so now just picking closest transform.position
            if (colliderAmountFound > 1)
            {
                for(int i = 1; i < interactables.Count; i++)
                {
                    if (Vector2.Distance(interactables[i].transform.position, touchPosition) < Vector2.Distance(closestCollider.transform.position, touchPosition))
                    {
                        closestCollider = interactables[i];
                    }
                }
            }

            if (closestCollider.TryGetComponent<IInteractable>(out IInteractable interactableScript))
            {
                if (interactableScript.InRange) 
                { 
                    interactableScript.Interact();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }

            else
            {
                Debug.LogWarning($"Missing IInteractable script from {closestCollider.gameObject.name}?");
                return false;
            }
        }
        return false;
    }
}
