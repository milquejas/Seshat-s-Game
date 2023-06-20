using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Pooling for this use case is overkill optimization
 * Possible problems later
 * 
 * Spawns both pooled draggable items 
 * And inventory items with amount indicated
 * Touch a inventory item and pooled WeightedItem appears into mouse position
 * This should spawn from possible inventory
 * Currently inventory is just TempInventoryList
 * 
 * Item returning calls ReturnItemToPool with fadeout and animation
 * 
*/
[System.Serializable]
public struct InventoryWeightedItem
{
    public ItemSO InventoryItemSO;
    public int ItemAmount;
}

public class ScaleMinigamePooler : MonoBehaviour
{
    [SerializeField] private int itemPoolAmount;
    [SerializeField] private AnimationCurve returnToPoolAnimationCurve;

    public DraggableWeightedItem ItemPrefab;
    public ScaleMinigameInventoryItem InventoryItemPrefab;

    [SerializeField] private List<DraggableWeightedItem> itemPool = new List<DraggableWeightedItem>();
    [SerializeField] private List<DraggableWeightedItem> itemsInUsePool = new List<DraggableWeightedItem>();
    private List<ScaleMinigameInventoryItem> inventoryItemsList = new List<ScaleMinigameInventoryItem>();
    public int ItemSortingOrder;

    private void Start()
    {
        for (int i = 0; i < itemPoolAmount; i++)
        {
            itemPool.Add(Instantiate(ItemPrefab));
        }
    }

    public void InitializeScaleInventory(List<InventoryWeightedItem> questInventoryList)
    {

        foreach (InventoryWeightedItem _inventoryWeightedItem in questInventoryList)
        {
            ScaleMinigameInventoryItem inventoryItem = Instantiate(InventoryItemPrefab, transform);
            inventoryItem.inventoryWeightedItem = _inventoryWeightedItem;
            
            inventoryItem.InitializeInventoryItem();
            inventoryItemsList.Add(inventoryItem);
        }
    }

    public void ResetEverything(bool onlyDraggables = false)
    {
        List<DraggableWeightedItem> inUseItems = new List<DraggableWeightedItem>(itemsInUsePool);

        foreach (DraggableWeightedItem item in inUseItems)
        {
            StartCoroutine(ReturnItemToPool(item, 0));
        }

        if (onlyDraggables) return;

        foreach (ScaleMinigameInventoryItem inventoryItem in inventoryItemsList)
        {
            Destroy(inventoryItem.gameObject);
        }
        inventoryItemsList.Clear();
    }

    public DraggableWeightedItem GetPooledItem()
    {
        DraggableWeightedItem selectedItem;

        // if pool is used up
        if (itemPool.Count == 0) 
        {
            Debug.Log("item pool has been used up...");
            selectedItem = itemsInUsePool[0];

            selectedItem.EnableItemCollider(false);
            selectedItem.originInventoryItem.gameObject.SetActive(true);
            selectedItem.originInventoryItem.inventoryWeightedItem.ItemAmount += 1;
            selectedItem.originInventoryItem.updateItemAmountText();

            itemsInUsePool.RemoveAt(0);
            itemsInUsePool.Add(selectedItem);

            return selectedItem;
        }

        selectedItem = itemPool[0];
        itemsInUsePool.Add(itemPool[0]);
        itemPool.Remove(itemPool[0]);

        return selectedItem;
    }
    
    public IEnumerator ReturnItemToPool(DraggableWeightedItem item, float duration)
    {
        item.RemoveFromCup();
        item.EnableItemCollider(false);
        item.RBody.velocity = Vector2.zero;
        item.originInventoryItem.inventoryWeightedItem.ItemAmount += 1;

        float elapsedTime = 0f;
        while (elapsedTime <= duration - 0.2f)
        {
            elapsedTime += Time.deltaTime;
            float percent = Mathf.Clamp01(elapsedTime / duration);
            float curvePercent = returnToPoolAnimationCurve.Evaluate(percent);

            item.itemImage.color = Color.LerpUnclamped(Color.white, new Color(1, 1, 1, 0), percent);
            item.transform.position = Vector2.LerpUnclamped(item.transform.position, item.originPoolPosition, curvePercent);
            yield return null;
        }

        itemPool.Add(item);
        itemsInUsePool.Remove(item);
        item.originInventoryItem.gameObject.SetActive(true);
        item.originInventoryItem.updateItemAmountText();

        item.gameObject.SetActive(false);
    }

    void OnMouseOver()
    {
        // Debug.Log($"Hovering over {gameObject.name}");
    }
}
