using System.Collections;
using UnityEngine;
using TMPro;

public class FiveQuestionsPuzzleManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public GameObject resultPanel;

    public string[] dialogueLines;
    private int currentLine;

    // Starts the dialogue with the given action onDialogueEnd
    public void StartDialogue(System.Action onDialogueEnd)
    {
        resultPanel.SetActive(true);
        StartCoroutine(DisplayDialogue(onDialogueEnd));
    }

    // Coroutine for displaying the dialogue
    IEnumerator DisplayDialogue(System.Action onDialogueEnd)
    {
        currentLine = 0;

        // Display each dialogue line with a 3-second delay
        while (currentLine < dialogueLines.Length)
        {
            resultText.text = dialogueLines[currentLine];
            currentLine++;

            yield return new WaitForSeconds(3f);
        }

        // Hide the dialogue box and invoke the given action
        resultPanel.SetActive(false);
        onDialogueEnd?.Invoke();
    }
}