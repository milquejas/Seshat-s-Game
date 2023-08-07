using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
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
    [SerializeField] private TaskSO ScaleMinigameTask;

    [SerializeField] private ConversationSO OneCupHelp;
    [SerializeField] private ConversationSO ScaleIsNotBalanced;
    [SerializeField] private ConversationSO UseMarkedWeightsOnly;
    [SerializeField] private ConversationSO WrongAmountOfItems;


    // TODO: Quests could be pulled from game manager on scene load
    [SerializeField] private ScaleMinigameQuestSO[] questOrder;
    private int currentQuest;

    [SerializeField] private List<ItemSO> weightItems;

    private void Awake()
    {
        // landscape orientation 
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
    private void Start()
    {
        dialog.DialogEndedEvent += DialogEnd;

        StartQuest();
    }
    private void OnDisable()
    {
        dialog.DialogEndedEvent -= DialogEnd;
    }

    private void DialogEnd(ConversationSO conversation)
    {
        questDescriptionContainer.SetActive(true);

        if (ScaleMinigameTask.Completed)
            LeaveScaleMinigame();
    }

    private void StartQuest()
    {
        currentQuest = ScaleMinigameTask.Progress;

        itemPooler.InitializeScaleInventory(questOrder[currentQuest].QuestPlayerInventory);

        if(questOrder[currentQuest].QuestStartDialogue != null)
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

    private bool ListHasWeights(List<ItemSO> itemList)
    {
        bool isWeights = false;
        
        foreach (ItemSO item in itemList)
        {
            if (item.ScaleWeight)
            {
                isWeights = true;
                goto end;
            }
                
        }
        end:
            return isWeights;
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
            dialog.StartConversation(OneCupHelp);
        }
    }

    // Place Balanced Quest means place weights and produce in different sides?
    private void ReadyPlaceBalancedQuest()
    {
        if (!scaleBehaviour.ScaleIsBalanced())
        {
            dialog.StartConversation(ScaleIsNotBalanced);
            return;
        }

        bool leftFitsQuest =
            ListFitsQuest(scaleBehaviour.leftCupItems);

        bool rightFitsQuest =
            ListFitsQuest(scaleBehaviour.rightCupItems);

        if ((leftFitsQuest && !ListHasWeights(scaleBehaviour.rightCupItems)) ||
            (rightFitsQuest && !ListHasWeights(scaleBehaviour.leftCupItems)) ||
            ListHasWeights(scaleBehaviour.leftCupItems) && ListHasWeights(scaleBehaviour.rightCupItems))
        {
            dialog.StartConversation(UseMarkedWeightsOnly);
            return;
        }

        if (!leftFitsQuest && !rightFitsQuest)
        {
            dialog.StartConversation(WrongAmountOfItems);
            return;
        }

        itemPooler.ResetEverything();
        ProgressQuest();
    }

    // free trading could have own button with trade text?
    private void ReadyFreeTradingQuest()
    {
        print("free trading not implemented");
    }

    private void ProgressQuest()
    {
        if (questOrder[currentQuest].LastQuestInRow)
        {
            // currentQuest++;
            dialog.StartConversation(ExitDialogue);

            ScaleMinigameTask.Completed = true;
            return;
        }

        currentQuest++;
        ScaleMinigameTask.Progress = currentQuest;
        StartQuest();
    }

    public void LeaveScaleMinigame()
    {
        GameManager.GameManagerInstance.LoadScene("IsometricNewFlow");
        SoundManager.Instance.ChangeBackgroundSong(1.5f, 0);
    }
}