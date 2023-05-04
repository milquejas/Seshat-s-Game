using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    ItemSO item;

    public Button RemoveButton;
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(ItemSO newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case ItemSO.ItemType.Apple:
                Player.Instance.IncreaseHealth(item.value);
                break;

            case ItemSO.ItemType.Citrus:
                Player.Instance.IncreaseHealth(item.value);
                break;
        }

        RemoveItem();
    }
}
