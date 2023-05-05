using System.Collections.Generic;
using UnityEngine;

/*
 * Touch this inventory item and pooled WeightedItem appears into mouse position
 * This should spawn from possible inventory
 * 
*/

public class ScaleMinigameInventoryItem : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public bool InRange { get; set; }

    [SerializeField] private Transform containerForPooledItems;
    public ItemSO inventoryProduce;
    private List<WeightedItem> itemList2 = new List<WeightedItem>();

    private void Start()
    {
        containerForPooledItems.GetComponentsInChildren(true, itemList2);
        foreach (WeightedItem item in itemList2)
        {

        }
    }

    public Transform Interact()
    {
        Debug.Log($"interacted with{gameObject.name}");
        
        return transform;
    }

    void OnMouseOver()
    {
        // Debug.Log($"Hovering over {gameObject.name}");
    }
}
