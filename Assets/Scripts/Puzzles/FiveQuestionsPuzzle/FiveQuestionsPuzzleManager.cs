using System.Collections;
using UnityEngine;
using TMPro;

public class FiveQuestionsPuzzleManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public GameObject resultPanel;

    public string[] ResultLines;
    private int currentLine;

    // Starts the dialogue with the given action onDialogueEnd
    public void RestartQuest(System.Action onDialogueEnd)
    {
        resultPanel.SetActive(true);
        StartCoroutine(DisplayQuest(onDialogueEnd));
    }

    // Coroutine for displaying the dialogue
    IEnumerator DisplayQuest(System.Action onDialogueEnd)
    {
        currentLine = 0;

        // Display each dialogue line with a 3-second delay
        while (currentLine < ResultLines.Length)
        {
            resultText.text = ResultLines[currentLine];
            currentLine++;

            yield return new WaitForSeconds(3f);
        }

        // Hide the dialogue box and invoke the given action
        resultPanel.SetActive(false);
        onDialogueEnd?.Invoke();
    }
}