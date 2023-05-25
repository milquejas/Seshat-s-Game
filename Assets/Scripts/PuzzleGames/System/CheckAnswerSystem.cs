using UnityEngine;

public class CheckAnswerSystem : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

    public bool CheckAnswer(int selectedOptionIndex, TaskComponent task)
    {
        return selectedOptionIndex == task.correctOptionIndex;
    }

    public void ShowFeedback(bool isCorrectAnswer, ScoreComponent score)
    {
        if (isCorrectAnswer)
        {
            score.playerScore++;
            dialogueSystem.StartDialogue(new DialogueComponent { dialogueLines = new string[] { "Correct!" } });
        }
        else
        {
            dialogueSystem.StartDialogue(new DialogueComponent { dialogueLines = new string[] { "Wrong answer." } });
        }
    }
}




