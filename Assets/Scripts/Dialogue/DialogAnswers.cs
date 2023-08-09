using UnityEngine;

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

    [SerializeField] private TaskListSO taskList;

    [SerializeField] private AllConversationsListSO allConversations;

    [SerializeField] private GameObject FiveQuestionPuzzle;

    [SerializeField] private FiveQuestionsPuzzleAnswers FiveQuestionsPuzzleAnswers;

    [SerializeField] private CatStatuePuzzle CatStatuePuzzle;

    [SerializeField] private MathQuess MathQuess;

    private void Start()
    {
        dialog = GetComponent<Dialog>();
    }

    public void AnswerReactions(string answerCase, NPCDialogStart npcDialogStarter)
    {
        switch (answerCase)
        {
                // way to completely exit conversation
            case "ExitCase":
                dialog.ExitDialog(false);
                break;

                // way to continue the same conversation after a question
            case "Continue":
                dialog.LineNumber++;
                dialog.ShowDialog();
                break;

                // example on how to change to specific different conversation mid dialogue
            case "ExampleDialogTree_PickedHard":
                dialog.StartConversation(allConversations.ConversationSO.Find(x => x.ConversationName == "NPCExampleDialogTreeHard"), npcDialogStarter);
                break;

            case "ExampleDialogTree_CorrectAnswer":
                dialog.StartConversation(allConversations.ConversationSO.Find(x => x.ConversationName == "NPCExampleDialogTreeSuccess"));
                
                npcDialogStarter.TaskCompleted();
                break;

            case "StartFiveQuestionsPuzzle":
                dialog.ExitDialog(true);
                FiveQuestionPuzzle.SetActive(true);
                break;

            case "StartFiveQuestionsPuzzleEasy":
                dialog.ExitDialog(true);
                FiveQuestionPuzzle.SetActive(true);
                FiveQuestionsPuzzleAnswers.StartFiveQuestionsPuzzle(FiveQuestionsPuzzleAnswers.AllPuzzles[0]);
                break;
            case "StartFiveQuestionsPuzzleMedium":
                dialog.ExitDialog(true);
                FiveQuestionPuzzle.SetActive(true);
                FiveQuestionsPuzzleAnswers.StartFiveQuestionsPuzzle(FiveQuestionsPuzzleAnswers.AllPuzzles[1]);
                break;
            case "StartFiveQuestionsPuzzleHard":
                dialog.ExitDialog(true);
                FiveQuestionPuzzle.SetActive(true);
                FiveQuestionsPuzzleAnswers.StartFiveQuestionsPuzzle(FiveQuestionsPuzzleAnswers.AllPuzzles[2]);
                break;
            case "FQEasyDivision":
                dialog.ExitDialog(true);
                FiveQuestionPuzzle.SetActive(true);
                FiveQuestionsPuzzleAnswers.StartFiveQuestionsPuzzle(FiveQuestionsPuzzleAnswers.AllPuzzles[3]);
                break;
            case "FQEasyMultiplication":
                dialog.ExitDialog(true);
                FiveQuestionPuzzle.SetActive(true);
                FiveQuestionsPuzzleAnswers.StartFiveQuestionsPuzzle(FiveQuestionsPuzzleAnswers.AllPuzzles[4]);
                break;
            case "FQEasyPercentage":
                dialog.ExitDialog(true);
                FiveQuestionPuzzle.SetActive(true);
                FiveQuestionsPuzzleAnswers.StartFiveQuestionsPuzzle(FiveQuestionsPuzzleAnswers.AllPuzzles[5]);
                break;

            case "StartTotemQuest":
                dialog.ExitDialog(true);
                MathQuess.StartTotemQuest();
                break;

            case "StartScaleAntiqueScene":
                GameManager.GameManagerInstance.LoadScene("AntiqueScaleGame");
                SoundManager.Instance.ChangeBackgroundSong(1.5f, 1);
                // change sounds here or inside scene?
                break;

            case "StartFruitBasketPuzzleScene":
                GameManager.GameManagerInstance.LoadScene("FruitBasketGame");
                SoundManager.Instance.ChangeBackgroundSong(1.5f, 2);
                // change sounds here or inside scene?
                break;

            case "StartStatuePuzzle":
                dialog.ExitDialog(true);
                CatStatuePuzzle.gameObject.SetActive(true);
                CatStatuePuzzle.InitializeCatStatuePuzzle(CatStatuePuzzle.AllPuzzles[0], npcDialogStarter.npcTask);
                break;

            case "StartGuessWeightsPuzzle":
                dialog.ExitDialog(true);
                CatStatuePuzzle.gameObject.SetActive(true);
                CatStatuePuzzle.InitializeCatStatuePuzzle(CatStatuePuzzle.AllPuzzles[1], npcDialogStarter.npcTask);
                break;

            default:
                Debug.LogWarning($"Typo in switch/conversation answer? {answerCase}");
                break;
        }
    }
}
