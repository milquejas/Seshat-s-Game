using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    public string[] dialogueLines;
    private int currentLine;

    // Starts the dialogue with the given action onDialogueEnd
    public void StartDialogue(System.Action onDialogueEnd)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(DisplayDialogue(onDialogueEnd));
    }

    // Coroutine for displaying the dialogue
    IEnumerator DisplayDialogue(System.Action onDialogueEnd)
    {
        currentLine = 0;

        // Display each dialogue line with a 3-second delay
        while (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
            currentLine++;

            yield return new WaitForSeconds(3f);
        }

        // Hide the dialogue box and invoke the given action
        dialogueBox.SetActive(false);
        onDialogueEnd?.Invoke();
    }
}
