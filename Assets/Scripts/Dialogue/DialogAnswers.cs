using UnityEngine;
/*
 * Dialog question execution happens here? 
 * Set case names in Conversation scriptable object and set what happens in this class
 * Names are case sensitive...
 * Painful way to make branching dialogs, but its possible. Needs heavy rethinking to make better. 
 * TODO optimization
 * 
*/

public class DialogAnswers : MonoBehaviour
{
    private Dialog dialog;
    [SerializeField] private ConversationListSO allConversations;

    private void Start()
    {
        dialog = GetComponent<Dialog>();
    }

    public void AnswerReactions(string answerCase)
    {
        switch (answerCase)
        {
                 // way to completely exit conversation
            case "ExitCase":
                dialog.ExitDialog();
                break;

                // way to continue the same conversation after a question
            case "Continue":
                dialog.lineNumber++;
                dialog.ShowDialog();
                break;

                // example on how to change to specific different conversation
            case "TestCase":
                dialog.StartConversation(allConversations.ConversationSO.Find(x => x.ConversationName == "KakkaConversation"));
                break;

            default:
                Debug.LogWarning($"Typo in switch/conversation answer? {answerCase}");
                break;
        }
    }
}
