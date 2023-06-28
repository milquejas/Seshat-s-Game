using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class FruitBasketManager : MonoBehaviour
{
    [SerializeField] private ScaleMinigamePooler basketPooler;
    [SerializeField] private List<ItemSO> basketItems = new();

    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private GameObject tooltipContainer;
    [SerializeField] private GameObject basketInfo;
    [SerializeField] private TMP_Text basketInfoText;

    [SerializeField] Button readyButton;

    private int currentQuest;
    [SerializeField] private FruitBasketQuestSO[] BasketPuzzleQuests;

    [SerializeField] private Dialog dialog;
    [SerializeField] private TaskSO[] FruitBasketTask;
    [SerializeField] private ConversationSO ExitDialogue;

    [SerializeField] private ConversationSO biggerValueDifference;
    [SerializeField] private ConversationSO grapelessMisery;
    [SerializeField] private ConversationSO smallValueDifference;
    [SerializeField] private ConversationSO tooHeavy;
    [SerializeField] private ConversationSO tooLight;
    [SerializeField] private ConversationSO wayTooHeavy;

    void Start()
    {
        ScaleMinigameInventoryItem.basketItemTouched += ShowTooltip;
        DraggableWeightedItem.DraggableItemTouched += ShowTooltip;
        readyButton.onClick.AddListener(CheckReadyButton);
        dialog.DialogEndedEvent += DialogEnd;

        StartQuest();
    }

    private void StartQuest()
    {
        currentQuest = GameManager.GameManagerInstance.CurrentPuzzleIndex;

        basketPooler.InitializeScaleInventory(BasketPuzzleQuests[currentQuest].BasketItems);

        if (BasketPuzzleQuests[currentQuest].StartingConversation != null)
        {
            dialog.StartConversation(BasketPuzzleQuests[currentQuest].StartingConversation);
        }
    }

    private void QuestComplete()
    {
        FruitBasketTask[currentQuest].Progress++;
        FruitBasketTask[currentQuest].Completed = true;
        dialog.StartConversation(ExitDialogue);
    }

    private void OnDisable()
    {
        ScaleMinigameInventoryItem.basketItemTouched -= ShowTooltip;
        DraggableWeightedItem.DraggableItemTouched -= ShowTooltip;
        dialog.DialogEndedEvent -= DialogEnd;
    }

    private void DialogEnd(ConversationSO conversation)
    {
        if (FruitBasketTask[currentQuest].Completed)
            LeaveFruitBasketScene();
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

    // Checks setup in order of importance
    private void CheckReadyButton()
    {
        var (totalValue, totalWeight) = CalculateBasketItems();
        int maxWeight = BasketPuzzleQuests[currentQuest].WeightLimit;
        int goalValue = BasketPuzzleQuests[currentQuest].ValueGoal;

        // way too heavy
        if (totalWeight > maxWeight + 250){
            dialog.StartConversation(wayTooHeavy);
            return;
        }
        // too heavy
        if (totalWeight > maxWeight){
            dialog.StartConversation(tooHeavy);
            return;
        }
        // way too light
        if (totalWeight < maxWeight - 300)
        {
            dialog.StartConversation(tooLight);
            return;
        }
        // if basket doesnt have grapes and list has grapes
        if (BasketPuzzleQuests[currentQuest].QuestHasGrapes && !basketItems.Any(item => item.ItemName == ItemType.Grapes))
        {
            dialog.StartConversation(grapelessMisery);
            return;
        }
        // value difference big
        if (totalValue < goalValue - 30)
        {
            dialog.StartConversation(biggerValueDifference);
            return;
        }
        // small value difference
        if (totalValue < goalValue)
        {
            dialog.StartConversation(smallValueDifference);
            return;
        }

        QuestComplete();
    }

    // calculation as tuple type to do less iteration on a list
    private (int value, int weight) CalculateBasketItems()
    {
        int totalValue = 0;
        int totalbasketWeight = 0;
        foreach (ItemSO item in basketItems)
        {
            totalbasketWeight += item.ItemWeight;
            totalValue += item.GoldValue;
        }
        return (totalValue, totalbasketWeight);
    }

    private void ShowBasketInfo()
    {
        var (totalValue, totalWeight) = CalculateBasketItems();
        if (totalWeight == 0)
        {
            basketInfo.SetActive(false);
            return;
        }
        basketInfo.SetActive(true);
        basketInfoText.text = $"Weight limit :{BasketPuzzleQuests[currentQuest].WeightLimit}g<br>Current " +
            $"Weight: {totalWeight}g<br>" +
            $"Value: {totalValue}";
    }

    private void ShowTooltip(ItemSO item)
    {
        tooltipContainer.SetActive(true);
        tooltipText.text = $"{item.ItemName}<br>Weight: {item.ItemWeight}g Value: {item.GoldValue} gold";
    }

    public void LeaveFruitBasketScene()
    {
        GameManager.GameManagerInstance.LoadScene("IsometricMain");
    }
}