using System;
using UnityEngine;

/*
 * Draggable
 * Animate while dragging?
 * click -> disable collider -> disable rigidbody physics -> 
 * Let go -> enable collider -> enable rigidbody physics ->
 * 
*/

[RequireComponent(typeof(WeightedItem))]
public class DraggableItem : MonoBehaviour, IInteractable
{
    [field: SerializeField] public bool InRange { get; set; }

    [field: NonSerialized] public WeightedItem weightedItem;

    private void Start()
    {
        weightedItem = GetComponent<WeightedItem>();
    }

    public Transform Interact()
    {
        return transform;
    }

    public void StartDragging(bool toggle)
    {
        weightedItem.EnableItemCollider(!toggle);
        weightedItem.RBody.gravityScale = Convert.ToInt32(!toggle);
    }
}

