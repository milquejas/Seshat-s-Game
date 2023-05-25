using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
/*
 * light starting reference:
 * https://github.com/draffauf/unity-dialogue-system/blob/master/Assets/Scripts/SpeakerUIController.cs 
 * Could add emotions to characters
 * Questions and dialog branching is badly implemented. 
 * Didn't think much how to make transitions for any dialog changes...
 *  
*/

public class Dialog : MonoBehaviour
{
    public Image Portrait;
    public TMP_Text SpeakerName;
    public TMP_Text dialog;
    public Image AnswerButtonPanel;
    public GameObject AnswerButton;

    private List<GameObject> answerButtons = new List<GameObject>();

    [SerializeField] private TouchMovementAndInteraction playerControls;

    public int lineNumber { get; set; }
    private bool answering;

    public Conversation CurrentConversation;
    private DialogAnswers dialogAnswers;

    private Character speaker;
    public Character Speaker
    {
        get => speaker;
        set
        {
            speaker = value;
            SpeakerName.text = speaker.CharacterName;
            Portrait.sprite = speaker.PortraitImage;
        }
    }

    private void Start()
    {
        dialogAnswers = GetComponent<DialogAnswers>();
    }

    public void StartConversation(Conversation _conversation)
    {
        gameObject.SetActive(true);
        playerControls.disableMovement = true;

        CurrentConversation = _conversation;
        lineNumber = 0;
        // transform.gameObject.SetActive(true);

        ShowDialog();
    }

    public void ExitDialog()
    {
        playerControls.disableMovement = false;
        gameObject.SetActive(false);
    }

    public void ShowDialog()
    {
        if (lineNumber >= CurrentConversation.Lines.Length)
        {
            ExitDialog();
            return;
        }

        if (CurrentConversation.Lines[lineNumber].LineType == DialogLineType.Question)
        {
            StartQuestion();
        }

        AdjustUIPositions();
        Speaker = CurrentConversation.Lines[lineNumber].character;
        dialog.text = CurrentConversation.Lines[lineNumber].dialogueText;
    }

    public void DialogClicked()
    {
        if (answering) return;

        lineNumber++;
        ShowDialog();
    }

    private void AdjustUIPositions()
    {
        switch (CurrentConversation.Lines[lineNumber].Position)
        {
            case CharacterPosition.Left:
                Portrait.gameObject.SetActive(true);
                Portrait.rectTransform.position = new Vector3(200f, Portrait.rectTransform.position.y, Portrait.rectTransform.position.z);
                break;

            case CharacterPosition.Right:
                Portrait.gameObject.SetActive(true);
                Portrait.rectTransform.position = new Vector3(1000f, Portrait.rectTransform.position.y, Portrait.rectTransform.position.z);
                break;

            case CharacterPosition.Middle:
                Portrait.gameObject.SetActive(true);
                Portrait.rectTransform.position = new Vector3(600f, Portrait.rectTransform.position.y, Portrait.rectTransform.position.z);
                break;

            case CharacterPosition.None:
                Portrait.gameObject.SetActive(false);
                break;

            default:
                Debug.LogWarning("current conversation position switch hit default, oh no panic?!");
                break;
        }
    }

    // question behaviour starts here
    private void StartQuestion()
    {
        answering = true;
        // creates all answer buttons and sets their 
        AnswerButtonPanel.gameObject.SetActive(true);
        foreach (Answer answer in CurrentConversation.Lines[lineNumber].LineAnswer)
        {
            GameObject newButton = Instantiate(AnswerButton, AnswerButtonPanel.transform);
            answerButtons.Add(newButton);

            newButton.transform.GetComponentInChildren<TMP_Text>().text = answer.AnswerText;
            newButton.GetComponent<Button>().onClick.AddListener(() => GiveAnswer(answer.AnswerSwitchCase));
        }
    }

    public void GiveAnswer(string answerCase)
    {
        DestroyUsedButtons();
        answering = false;

        AnswerButtonPanel.gameObject.SetActive(false);

        dialogAnswers.AnswerReactions(answerCase);
    }

    // destroy buttons and empty list, remove listeners just in case to clean memory leaks
    private void DestroyUsedButtons()
    {
        foreach (GameObject oldButton in answerButtons)
        {
            oldButton.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(oldButton);
        }
        answerButtons.Clear();
    }
}
    
