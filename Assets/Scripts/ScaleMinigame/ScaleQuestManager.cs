using System;
using System.Collections.Generic;
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

    [SerializeField] private GameObject FadeOutCanvas;

    private ScaleCup? produceCup = null;

    // TODO: Quests could be pulled from game manager on scene load
    [SerializeField] private ScaleMinigameQuestSO[] questOrder;
    private int currentQuest;

    private void Start()
    {
        // Load scene async? For fadeout during enter
        // https://forum.unity.com/threads/what-and-how-is-the-best-way-to-fade-in-out-when-loading-switching-scenes.1388280/
        //dialog.ConversationEnded += CheckConversation;
        Dialog.DialogEndedEvent += DialogEnd;
    }

    private void DialogEnd(ConversationSO conversation)
    {
        StartQuest();
        FadeOutCanvas.SetActive(true);

    }

    private void StartQuest()
    {
        itemPooler.InitializeScaleInventory(questOrder[currentQuest].QuestPlayerInventory);
    }

    public void ProducePlacedInCup(ScaleCup cup)
    {
        if (CheckIfWrongCup(cup))
        {
            // todo false message to player: "pick one cup" or place weights on the other side
        }
    }

    private bool CheckIfWrongCup(ScaleCup cup)
    {
        if (questOrder[currentQuest].QuestType == ScaleMinigameQuestSO.ScaleQuestType.FreeTrading) return false;

        if (produceCup is null)
        {
            produceCup = cup;
        }

        if (cup == produceCup) return false;

        return true;
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