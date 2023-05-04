using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemSO> Items = new List<ItemSO>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemSO item)
    {
        Items.Add(item);
    }

    public void Remove(ItemSO item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        // Clean content

        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemImage = obj.transform.Find("ItemImage").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemImage.sprite = item.itemImage;

            if (EnableRemove.isOn)
                removeButton.gameObject.SetActive(true);
        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }           
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }
    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        if (InventoryItems != null && InventoryItems.Length > 0)
        {
            for (int i = 0; i < Items.Count && i < InventoryItems.Length; i++)
            {
                InventoryItems[i].AddItem(Items[i]);
            }
        }
    }

    //public void SetInventoryItems()
    //{
    //    InventoryItems = new InventoryItemController[Items.Count];
    //    for (int i = 0; i < Items.Count; i++)
    //    {
    //        InventoryItems[i] = ItemContent.GetChild(i).GetComponent<InventoryItemController>();
    //        InventoryItems[i].AddItem(Items[i]);
    //    }
    //}

    //public void SetInventoryItems()
    //{
    //    InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

    //    for (int i = 0; i < Items.Count -1; i++)
    //    {
    //        InventoryItems[i].AddItem(Items[i]);
    //    }
    //}

}
