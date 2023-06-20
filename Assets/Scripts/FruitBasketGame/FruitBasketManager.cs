using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 

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
    [SerializeField]
    private Button readyButton;
    private int weightLimit = 720;
    private int valueGoal = 280;



    void Start()
    {
        basketPooler.InitializeScaleInventory(puzzleItems);
        ScaleMinigameInventoryItem.basketItemTouched += ShowTooltip;
        DraggableWeightedItem.DraggableItemTouched += ShowTooltip;
        readyButton.onClick.AddListener(CheckReadyButton);
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
    private void CheckReadyButton()
    {
        int totalWeight = CalculateBasketWeight();
        int totalValue = CalculateBasketValue();

        List<string> messages = new List<string>();

        if (totalWeight > weightLimit)
        {
            messages.Add("The basket is too heavy! Try removing some fruits.");
        }
        else if (totalWeight < weightLimit)
        {
            messages.Add("You can fit in more fruits.");
            if (totalValue < valueGoal)
            {
                messages.Add("The basket's value is not enough!");
            }
        }

        string message = messages.Count > 0 ? string.Join(" ", messages) : "The target has been achieved and the basket is ready. Congratulations!";
        tooltipText.text = message;
        tooltipContainer.SetActive(true);
    }





    private int CalculateBasketValue()
    {
        int totalValue = 0;
        foreach (ItemSO item in basketItems)
        {
            totalValue += item.GoldValue;
        }
        return totalValue;
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
        basketInfoText.text = $"Weight limit :{weightLimit}g<br>Current Weight: {CalculateBasketWeight()}g";
    }

    private void ShowTooltip(ItemSO item)
    {
        tooltipContainer.SetActive(true);
        tooltipText.text = $"{item.ItemName}<br>Weight: {item.ItemWeight}g Value: {item.GoldValue} gold";

    }
}