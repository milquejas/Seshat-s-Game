using TMPro;
using UnityEngine;

public class DialogueDisplaySystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    public void UpdateDisplay(DialogueComponent dialogue)
    {
        dialogueText.text = dialogue.dialogueLines[0];
        dialogueBox.SetActive(true);
    }

    public void HideDialogueBox()
    {
        dialogueBox.SetActive(false);
    }
}
