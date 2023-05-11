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
    public InventoryWeightedItem inventoryWeightedItem;
    [SerializeField] private ScaleMinigamePooler inventoryPooler;
    private DraggableWeightedItem selectedItem;

    private SpriteRenderer itemImage;

    public void InitializeInventoryItem()
    {
        inventoryPooler = GetComponentInParent<ScaleMinigamePooler>();
        itemImage = GetComponent<SpriteRenderer>();
        itemImage.sprite = inventoryWeightedItem.ItemType.ItemImage;
    }

    public Transform Interact()
    {
        selectedItem = inventoryPooler.GetPooledItem();
        selectedItem.Item = inventoryWeightedItem.ItemType;
        selectedItem.gameObject.SetActive(true);
        selectedItem.originPoolPosition = transform.position;
        selectedItem.originInventoryItem = this;

        inventoryWeightedItem.ItemAmount -= 1;

        if (inventoryWeightedItem.ItemAmount <= 0 )
        {
            transform.gameObject.SetActive(false);
        }

        return selectedItem.transform;
    }

    void OnMouseOver()
    {
        // Debug.Log($"Hovering over {gameObject.name}");
    }
}
