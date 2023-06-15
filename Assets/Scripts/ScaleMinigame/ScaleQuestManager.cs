using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * Check if inventory has exact quest items
 * List of InventoryWeightedItem for each weight puzzle. 
 * Enter puzzle, start specific puzzle
 * Populate ScaleMinigamePooler with puzzle phase specific items
 * Give proper dialogue to specific phases of puzzle
 * Give dialogue to wrong and right answers
 * Progress Scale
 * 
 * "Place 3 oranges on one side of the scale, then press ready"
 * "Very good, next place 300g on the other side, then press ready"
*/

// Quest has inventory item list, which cup is active, quest tooltip text, 
// what happens after quest?

/*
 * If freetrade cup weight =/= cup2 weight, popup and say scale not equal please fix
 * If cup has weights and produce, popup and say don't mix weights and produce
 * If quest is a place produce or place weights phase and player puts stuff on wrong side, take back to pool and popup and say pls stop
 * 
 */
public class ScaleQuestManager : MonoBehaviour
{
    [SerializeField] private Button readyButton;
    [SerializeField] private Dialog dialog;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject questDescriptionContainer;
    [SerializeField] private TMP_Text questDescription;
    [SerializeField] private Image questArrowPointerImage;
    [SerializeField] private GameObject tradeRatioContainer;
    [SerializeField] private TMP_Text tradeRatios;    

    [SerializeField] private ScaleMinigamePooler itemPooler;
    [SerializeField] private ScaleBehaviour scaleBehaviour;

    [SerializeField] private GameObject FadeOutCanvas;
    [SerializeField] private ConversationSO ExitDialogue;

    // TODO: Quests could be pulled from game manager on scene load
    [SerializeField] private ScaleMinigameQuestSO[] questOrder;
    private int currentQuest;

    [SerializeField] private List<ItemSO> weightItems;
    private void Start()
    {
        // Load scene async? For fadeout during enter
        // https://forum.unity.com/threads/what-and-how-is-the-best-way-to-fade-in-out-when-loading-switching-scenes.1388280/
        //dialog.ConversationEnded += CheckConversation;

        dialog.DialogEndedEvent += DialogEnd;

        StartQuest();
    }

    private void DialogEnd(ConversationSO conversation)
    {
        // StartQuest();
        // FadeOutCanvas.SetActive(true);
        questDescriptionContainer.SetActive(true);
        
    }

    private void StartQuest()
    {
        itemPooler.InitializeScaleInventory(questOrder[currentQuest].QuestPlayerInventory);

        if(questOrder[currentQuest].QuestStartDialogue is not null)
        {
            dialog.StartConversation(questOrder[currentQuest].QuestStartDialogue);
            questDescriptionContainer.SetActive(false);
        }

        questDescription.text = questOrder[currentQuest].Description;
    }

    // when trade is pressed... 
    // List<ItemSO> PlaceTheseProduce
    // ScaleBehaviour.leftCupItems
    // ScaleBehaviour.rightCupItems
    public void ReadyButtonPressed()
    {
        if (questOrder[currentQuest].QuestType == ScaleMinigameQuestSO.ScaleQuestType.PlaceOnOneCup) 
            ReadyPlaceOnOneCupQuest();
        
        else if (questOrder[currentQuest].QuestType == ScaleMinigameQuestSO.ScaleQuestType.PlaceBalanced) 
            ReadyPlaceBalancedQuest();

        else if (questOrder[currentQuest].QuestType == ScaleMinigameQuestSO.ScaleQuestType.FreeTrading) 
            ReadyFreeTradingQuest();
    }

    private bool ListIsWeights(List<ItemSO> itemList)
    {
        foreach (ItemSO item in itemList)
        {
            if (!item.Weight)
                return false;
        }
        return true;
    }

    private bool ListFitsQuest(List<ItemSO> itemList)
    {
        return itemList.OrderBy(m => m.ItemName).SequenceEqual(questOrder[currentQuest].PlaceTheseProduce.OrderBy(m => m.ItemName));
    }

    private void ReadyPlaceOnOneCupQuest()
    {
        bool leftIsEmptyList = scaleBehaviour.leftCupItems.Count == 0;
        bool rightIsEmptyList = scaleBehaviour.rightCupItems.Count == 0;
        bool leftFitsQuest =
            ListFitsQuest(scaleBehaviour.leftCupItems);

        bool rightFitsQuest =
            ListFitsQuest(scaleBehaviour.rightCupItems);

        if ((leftFitsQuest && rightIsEmptyList) ||
            (rightFitsQuest && leftIsEmptyList))
        {
            ProgressQuest();
        }

        else
        {
            print("try again!?");
        }
    }

    // Place Balanced Quest means place weights and produce in different sides?
    private void ReadyPlaceBalancedQuest()
    {
        if (!scaleBehaviour.ScaleIsBalanced())
        {
            print("Scale is not balanced!");
            return;
        }

        bool leftFitsQuest =
            ListFitsQuest(scaleBehaviour.leftCupItems);

        bool rightFitsQuest =
            ListFitsQuest(scaleBehaviour.rightCupItems);

        if (!leftFitsQuest && !rightFitsQuest)
        {
            print("Place the right items in a cup");
            return;
        }

        if ((leftFitsQuest && !ListIsWeights(scaleBehaviour.rightCupItems)) || 
            (rightFitsQuest && !ListIsWeights(scaleBehaviour.leftCupItems)))
        {
            print("Use only marked weights to balance the scale");
            return;
        }

        itemPooler.ResetEverything();
        ProgressQuest();
    }

    // free trading could have own button with trade text?
    private void ReadyFreeTradingQuest()
    {

    }

    private void ProgressQuest()
    {
        if (questOrder[currentQuest].LastQuestInRow)
        {
            currentQuest++;
            dialog.StartConversation(ExitDialogue);
            GameManager.GameManagerInstance.LoadScene("IsometricMain");
        }

        Debug.Log("success progressing quest");
        currentQuest++;
        StartQuest();
    }

    private void CheckConversation(ConversationSO conversation)
    {
        switch (conversation.ConversationName)
        {
            case "TutorialEnter":
                
                break;
        }
    }
}