using UnityEngine;

/*
 * Check if inventory has exact quest items
 * List of InventoryWeightedItem for each weight puzzle. 
 * Enter puzzle, start specific puzzle
 * Populate ScaleMinigamePooler with puzzle phase specific items
 * Give proper dialogue to specific phases of puzzle
 * Give dialogue to wrong and right answers
 * Progress Scale
*/

// Quest has inventory item list, 
public class Quest
{
    public string description;
    public ItemType itemType;
    public float targetWeight;
}

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Dialog dialog;

    private void Start()
    {
        dialog.ConversationEnded += CheckConversation;
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