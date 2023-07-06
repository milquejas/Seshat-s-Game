using System.Collections;
using UnityEngine;
using TMPro;

public class FiveQuestionsPuzzleManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public GameObject resultPanel;
    public GameObject questionPanel; // Lisätty viittaus QuestionPanel-objektiin

    public string[] resultLines;
    private int currentLine;

    // Starts the dialogue with the given action onDialogueEnd
    public void RestartQuest(System.Action onDialogueEnd)
    {
        resultPanel.SetActive(true);
        questionPanel.SetActive(false); // Piilota QuestionPanel-objekti
        StartCoroutine(DisplayQuest(onDialogueEnd));
    }

    // Coroutine for displaying the dialogue
    IEnumerator DisplayQuest(System.Action onDialogueEnd)
    {
        currentLine = 0;

        // Display each dialogue line with a 3-second delay
        while (currentLine < resultLines.Length)
        {
            resultText.text = resultLines[currentLine];
            currentLine++;

            yield return new WaitForSeconds(3f);
        }

        // Hide the dialogue box and invoke the given action
        resultPanel.SetActive(false);
        questionPanel.SetActive(true); // Näytä QuestionPanel-objekti uudelleen
        onDialogueEnd?.Invoke();
    }
}
