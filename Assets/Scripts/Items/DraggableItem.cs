using UnityEngine;

/*
 * Draggable
*/

public class DraggableItem : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public bool InRange { get; set; }

    public Transform Interact()
    {
        Debug.Log($"interacted with{gameObject.name}");
        return transform;
    }
}
