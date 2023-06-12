using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    ItemSOMike item;

    public Button RemoveButton;
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(ItemSOMike newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case ItemSOMike.ItemTypeMike.Apple:
                Player.Instance.IncreaseHealth(item.value);
                break;

            case ItemSOMike.ItemTypeMike.Citrus:
                Player.Instance.IncreaseHealth(item.value);
                break;
        }

        RemoveItem();
    }
}
