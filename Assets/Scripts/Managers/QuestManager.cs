using UnityEngine;

/*
 * Check if inventory has exact quest items
 * 
*/

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
                // Debug.Log("test");
                break;
        }
    }
}