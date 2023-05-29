using System.Collections.Generic;
using UnityEngine;

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

public class ScaleQuest
{
    public string description;
    public ItemType itemType;
    public float targetWeight;
    public List<ItemSO> CupOneTarget;
    public List<ItemSO> CupTwoWeights;
}

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Dialog dialog;

    private void Start()
    {
        //dialog.ConversationEnded += CheckConversation;
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