using System.Collections.Generic;
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