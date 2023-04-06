using System.Collections.Generic;
using UnityEngine;

/* 
 * For trying to interact with objects in level
*/

public static class InteractSystem
{
    public static bool CanInteract;

    private static LayerMask grabbableLayer = LayerMask.GetMask("GrabbableObject");
    private static ContactFilter2D grabbableContactFilter;

    private static List<Collider2D> Interactables = new List<Collider2D>();

    public static void InitContactFilters()
    {
        grabbableContactFilter.SetLayerMask(grabbableLayer);
    }

    public static GrabbableObject TryToInteract(Vector2 position, float size)
    {
        int colliderAmountFound = Physics2D.OverlapCircle(position, size, grabbableContactFilter, Interactables);

        if (colliderAmountFound > 0)
        {
            if (Interactables[0].gameObject.TryGetComponent(out GrabbableObject grabbable))
            {
                if (grabbable.HasBeenGrabbed) return null;

                grabbable.HasBeenGrabbed = true;
                
                return grabbable;
            }
        }
        return null;
    }
}
