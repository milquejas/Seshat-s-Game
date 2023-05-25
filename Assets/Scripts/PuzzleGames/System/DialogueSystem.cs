using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    private string[] currentDialogueLines;
    private int currentLineIndex;
    private bool isDialogueActive;

    public void StartDialogue(DialogueComponent dialogue)
    {
        currentDialogueLines = dialogue.dialogueLines;
        currentLineIndex = 0;
        isDialogueActive = true;
        dialogueBox.SetActive(true);
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (isDialogueActive)
        {
            if (currentLineIndex < currentDialogueLines.Length)
            {
                dialogueText.text = currentDialogueLines[currentLineIndex];
                currentLineIndex++;
            }
            else
            {
                EndDialogue();
            }
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        isDialogueActive = false;
        // Voit toteuttaa tarvittavat toiminnot dialogin lopettamiseksi
    }
}

