using System.Collections;
using System.Collections.Generic;
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


    void Start()
    {
        basketPooler.InitializeScaleInventory(puzzleItems);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<DraggableWeightedItem>())

        {
            DraggableWeightedItem draggedItem = collision.gameObject.GetComponentInParent<DraggableWeightedItem>();
            basketItems.Add(draggedItem.Item);
            
            
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
