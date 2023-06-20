using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FruitBasketManager : MonoBehaviour
{
    [SerializeField]
    private ScaleMinigamePooler basketPooler;
    [SerializeField]
    private List<InventoryWeightedItem> puzzleItems;
    [SerializeField]
    private List<ItemSO> basketItems = new List<ItemSO>();
    [SerializeField]
    private TMP_Text tooltipText;
    [SerializeField]
    private GameObject tooltipContainer;
    [SerializeField]
    private GameObject basketInfo;
    [SerializeField]
    private TMP_Text basketInfoText;
    private int maxWeight = 1250;
    


    void Start()
    {
        basketPooler.InitializeScaleInventory(puzzleItems);
        ScaleMinigameInventoryItem.basketItemTouched += ShowTooltip;
        DraggableWeightedItem.DraggableItemTouched += ShowTooltip;
    }

    private void OnDisable()
    {
        ScaleMinigameInventoryItem.basketItemTouched -= ShowTooltip;
        DraggableWeightedItem.DraggableItemTouched -= ShowTooltip;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<DraggableWeightedItem>())

        {
            DraggableWeightedItem draggedItem = collision.gameObject.GetComponentInParent<DraggableWeightedItem>();
            basketItems.Add(draggedItem.Item);
            ShowBasketInfo();


        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<DraggableWeightedItem>())

        {
            DraggableWeightedItem draggedItem = collision.gameObject.GetComponentInParent<DraggableWeightedItem>();
            basketItems.Remove(draggedItem.Item);
            ShowBasketInfo();

        }
    }

    private int CalculateBasketWeight()
    {
        int totalbasketWeight = 0;
        foreach (ItemSO item in basketItems)
        {
            totalbasketWeight += item.ItemWeight;
        }
        return totalbasketWeight;

        
    }
    private void ShowBasketInfo()
    {
        if (CalculateBasketWeight() == 0)
        {
            basketInfo.SetActive(false);
            return;
        }
            basketInfo.SetActive(true);
        basketInfoText.text = $"Weight limit :{maxWeight}g<br>Current Weight: {CalculateBasketWeight()}g";
    }

    private void ShowTooltip(ItemSO item)
    {
        tooltipContainer.SetActive(true);
        tooltipText.text = $"{item.ItemName}<br>Weight: {item.ItemWeight}g Value: {item.GoldValue} gold";

    }
}
