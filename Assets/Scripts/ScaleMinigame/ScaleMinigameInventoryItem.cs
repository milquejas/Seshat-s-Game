using System;
using TMPro;
using UnityEngine;

/*
 * Touch this inventory item and pooled WeightedItem appears into mouse position
 * Sets up pooled items type
*/

public class ScaleMinigameInventoryItem : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public bool InRange { get; set; }
    public InventoryWeightedItem inventoryWeightedItem;
    [SerializeField] private ScaleMinigamePooler inventoryPooler;
    private DraggableWeightedItem selectedItem;
    [SerializeField] private TextMeshPro amountText;

    private SpriteRenderer itemImage;

    public static event Action<ItemSO> basketItemTouched;

    public void InitializeInventoryItem()
    {
        inventoryPooler = GetComponentInParent<ScaleMinigamePooler>();
        itemImage = GetComponent<SpriteRenderer>();
        itemImage.sprite = inventoryWeightedItem.ItemType.ItemImage;
        updateItemAmountText();
    }

    public Transform Interact()
    {
        selectedItem = inventoryPooler.GetPooledItem();

        selectedItem.gameObject.SetActive(true);
        selectedItem.originPoolPosition = transform.position;
        selectedItem.originInventoryItem = this;

        inventoryWeightedItem.ItemAmount -= 1;

        updateItemAmountText();
        selectedItem.InitializeWeightedItem(inventoryWeightedItem.ItemType);

        basketItemTouched?.Invoke(inventoryWeightedItem.ItemType);
        return selectedItem.transform;
    }

    public void updateItemAmountText()
    {
        amountText.text = inventoryWeightedItem.ItemAmount.ToString();

        if (inventoryWeightedItem.ItemAmount <= 0)
        {
            transform.gameObject.SetActive(false);
        }
        if (inventoryWeightedItem.ItemAmount == 1)
            amountText.text = null;
    }

    void OnMouseOver()
    {
        // Debug.Log($"Hovering over {gameObject.name}");
    }
}
