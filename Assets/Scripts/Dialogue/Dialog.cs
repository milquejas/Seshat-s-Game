using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

/*
* starting reference:
* https://github.com/draffauf/unity-dialogue-system/blob/master/Assets/Scripts/SpeakerUIController.cs 
* Could add emotions to characters
* Questions and dialog branching is badly implemented. 
* Didn't think much how to make transitions for any dialog changes...
* 
* Static event action DialogEndedEvent when dialog ends
*/

public class Dialog : MonoBehaviour
{
    public event Action<ConversationSO> DialogEndedEvent;

    [SerializeField] private GameObject canvas;
    [SerializeField] private Image Portrait;
    [SerializeField] private TMP_Text SpeakerName;
    [SerializeField] private Image SpeakerNameBox;
    [SerializeField] private TMP_Text dialog;
    [SerializeField] private Image AnswerButtonPanel;
    [SerializeField] private GameObject AnswerButton;

    private List<GameObject> answerButtons = new List<GameObject>();

    public int lineNumber { get; set; }
    private bool answering;

    public ConversationSO CurrentConversation;
    private DialogAnswers dialogAnswers;

    [Header("GameObject with player controls")]
    [SerializeField] private GameObject playerControls;
    private IPlayerTouch playerInteraction;

    private CharacterSO speaker;
    public CharacterSO Speaker
    {
        get => speaker;
        set
        {
            speaker = value;
            SpeakerName.text = speaker.CharacterName;
            Portrait.sprite = speaker.PortraitImage;
        }
    }

    private void Awake()
    {
        dialogAnswers = GetComponent<DialogAnswers>();
        playerInteraction = playerControls.GetComponent<IPlayerTouch>();
    }

    public void StartConversation(ConversationSO _conversation)
    {
        canvas.SetActive(true);
        playerInteraction.DisablePlayerMovement(true);

        CurrentConversation = _conversation;
        lineNumber = 0;
        // transform.gameObject.SetActive(true);

        ShowDialog();
    }

    public void ExitDialog(bool disableControls)
    {
        playerInteraction.DisablePlayerMovement(disableControls);
        canvas.SetActive(false);
        
        // Event launched when dialog ends
        DialogEndedEvent?.Invoke(CurrentConversation);
    }

    public void ShowDialog()
    {
        if (lineNumber >= CurrentConversation.Lines.Length)
        {
            ExitDialog(false);
            return;
        }

        if (CurrentConversation.Lines[lineNumber].LineType == DialogLineType.Question)
        {
            StartQuestion();
        }

        AdjustUIPositions();

        if (CurrentConversation.Lines[lineNumber].character is null)
        {
            SpeakerNameBox.gameObject.SetActive(false);
            Portrait.gameObject.SetActive(false);
            SpeakerName.gameObject.SetActive(false);
        }
        else
        {
            SpeakerNameBox.gameObject.SetActive(true);
            Portrait.gameObject.SetActive(true);
            SpeakerName.gameObject.SetActive(true);
            Speaker = CurrentConversation.Lines[lineNumber].character;
        }

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
                Debug.LogWarning("current conversation position switch hit default, debug?!");
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
    
