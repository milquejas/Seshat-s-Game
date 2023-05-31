using UnityEngine;
using UnityEngine.SceneManagement;
/*
* Dialog question execution happens here? 
* Set case names in Conversation scriptable object and set what happens in this class
* Names are case sensitive...
* Painful way to make branching dialogs, but its possible. Needs heavy rethinking to make better. 
* 
*/

public class DialogAnswers : MonoBehaviour
{
    private Dialog dialog;
    [SerializeField] private ConversationListSO allConversations;
    [SerializeField] private GameObject Puzzle01;
    [SerializeField] private GameObject FiveQuestionPuzzle;
    [SerializeField] private ThreeMultipickPuzzle ThreeMultipickPuzzle;

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
                dialog.ExitDialog(false);
                break;

                // way to continue the same conversation after a question
            case "Continue":
                dialog.lineNumber++;
                dialog.ShowDialog();
                break;

                // example on how to change to specific different conversation mid dialogue
            case "ExampleDialogTree_PickedHard":
                dialog.StartConversation(allConversations.ConversationSO.Find(x => x.ConversationName == "NPCExampleDialogTreeHard"));
                break;

            case "ExampleDialogTree_CorrectAnswer":
                dialog.StartConversation(allConversations.ConversationSO.Find(x => x.ConversationName == "NPCExampleDialogTreeSuccess"));
                break;

            case "StartStatuePuzzle":
                dialog.ExitDialog(true);
                Puzzle01.SetActive(true);
                break;

            case "StartFiveQuestionsPuzzle":
                dialog.ExitDialog(true);
                FiveQuestionPuzzle.SetActive(true);
                break;

            case "StartScaleAntiqueScene":
                SceneManager.LoadScene("QuessAnswer");
                break;

            case "StartThreeMultipickPuzzle":
                dialog.ExitDialog(true);
                ThreeMultipickPuzzle.gameObject.SetActive(true);
                ThreeMultipickPuzzle.InitializeThreeMultipickPuzzle(ThreeMultipickPuzzle.allPuzzles[0]);
                break;

            case "StartGuessWeightsPuzzle":
                dialog.ExitDialog(true);
                ThreeMultipickPuzzle.gameObject.SetActive(true);
                ThreeMultipickPuzzle.InitializeThreeMultipickPuzzle(ThreeMultipickPuzzle.allPuzzles[1]);
                break;

            default:
                Debug.LogWarning($"Typo in switch/conversation answer? {answerCase}");
                break;
        }
    }
}
