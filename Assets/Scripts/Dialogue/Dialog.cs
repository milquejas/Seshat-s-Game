using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.DeviceSimulation;

/*
 * ref:
 * https://github.com/draffauf/unity-dialogue-system/blob/master/Assets/Scripts/SpeakerUIController.cs 
 * Could add emotions to characters
 * 
*/

public class Dialog : MonoBehaviour
{
    public Image Portrait;
    public TMP_Text SpeakerName;
    public TMP_Text dialog;

    private int lineNumber;

    private Character speaker;
    public Conversation CurrentConversation;
    public Character Speaker
    {
        get { return speaker; }
        set
        {
            speaker = value;
            SpeakerName.text = speaker.CharacterName;
            Portrait.sprite = speaker.PortraitImage;
        }
    }

    private void Start()
    {
        StartConversation(CurrentConversation);
    }

    public void StartConversation(Conversation _conversation)
    {
        CurrentConversation = _conversation;
        gameObject.SetActive(true);
        lineNumber = 0;
        // transform.gameObject.SetActive(true);

        ShowDialog();
    }

    public void DialogClicked()
    {
        
        if (lineNumber >= CurrentConversation.Lines.Length)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Speaker = CurrentConversation.Lines[lineNumber].character;
            dialog.text = CurrentConversation.Lines[lineNumber].dialogueText;
            Debug.Log("test");
        }
    }

    private void ShowDialog()
    {
        if (lineNumber >= CurrentConversation.Lines.Length)
        {
            gameObject.SetActive(false);
            return;
        }

        Speaker = CurrentConversation.Lines[lineNumber].character;
        dialog.text = CurrentConversation.Lines[lineNumber].dialogueText;

        lineNumber++;
    }

    private void AdjustUIPositions()
    {
        if (CurrentConversation.Lines[lineNumber].LineType != DialogLineType.Dialog)
        {
            Debug.LogWarning("not implemented");
            return;
        }

        switch (CurrentConversation.Lines[lineNumber].Position)
        {
            case CharacterPosition.Left:

                break;

            case CharacterPosition.Right:

                break;

            case CharacterPosition.Middle:

                break;

            case CharacterPosition.None:

                break;

            default:
                Debug.LogWarning("why is this here panic?");
                break;
        }
    }
}
