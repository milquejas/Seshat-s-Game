using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

/*
* starting reference:
* https://github.com/draffauf/unity-dialogue-system/blob/master/Assets/Scripts/SpeakerUIController.cs 
* 
* Create ConversationSO ->
* Call referenced dialog.StartConversation(ConversationSO) to start a dialog in scene
* 
* Static event action DialogEndedEvent when dialog ends
* Takes optional NPCDialogStart class as parameter to complete tasks
*/

public class Dialog : MonoBehaviour
{
    public event Action<ConversationSO> DialogEndedEvent;

    [SerializeField] private GameObject canvas;
    [SerializeField] private Image Portrait;
    [SerializeField] private TMP_Text SpeakerName;
    [SerializeField] private Image CharacterContainer;
    [SerializeField] private TMP_Text dialog;
    [SerializeField] private Image AnswerButtonPanel;
    [SerializeField] private GameObject AnswerButton;

    private List<GameObject> answerButtons = new();

    public int LineNumber { get; set; }
    private bool answering;

    public ConversationSO CurrentConversation;
    private DialogAnswers dialogAnswers;
    private NPCDialogStart npcDialogStarter;

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

    public void StartConversation(ConversationSO _conversation, NPCDialogStart _npcDialogStarter = null)
    {
        npcDialogStarter = _npcDialogStarter;
        canvas.SetActive(true);
        playerInteraction.DisablePlayerMovement(true);

        CurrentConversation = _conversation;
        LineNumber = 0;
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
        if (LineNumber >= CurrentConversation.Lines.Length)
        {
            ExitDialog(false);
            return;
        }

        if (CurrentConversation.Lines[LineNumber].LineType == DialogLineType.Question)
        {
            StartQuestion();
        }

        AdjustUIPositions();

        if (CurrentConversation.Lines[LineNumber].character == null )
        {
            CharacterContainer.gameObject.SetActive(false);
            Portrait.gameObject.SetActive(false);
            SpeakerName.gameObject.SetActive(false);
        }
        else if (CurrentConversation.Lines[LineNumber].character.PortraitImage == null)
        {
            CharacterContainer.gameObject.SetActive(true);
            Portrait.gameObject.SetActive(false);
            SpeakerName.gameObject.SetActive(true);
            Speaker = CurrentConversation.Lines[LineNumber].character;
        }
        else
        {
            CharacterContainer.gameObject.SetActive(true);
            Portrait.gameObject.SetActive(true);
            SpeakerName.gameObject.SetActive(true);
            Speaker = CurrentConversation.Lines[LineNumber].character;
        }

        dialog.text = CurrentConversation.Lines[LineNumber].dialogueText;
    }

    public void DialogClicked()
    {
        if (answering) return;

        LineNumber++;
        ShowDialog();
    }

    private void AdjustUIPositions()
    {
        switch (CurrentConversation.Lines[LineNumber].Position)
        {
            case CharacterPosition.Left:
                CharacterContainer.rectTransform.anchorMin = new Vector2(0, 1);
                CharacterContainer.rectTransform.anchorMax = new Vector2(0, 1);
                CharacterContainer.rectTransform.anchoredPosition = new Vector3(274.3f, 0, 0);
                break;

            case CharacterPosition.Right:
                CharacterContainer.rectTransform.anchorMin = new Vector2(1, 1);
                CharacterContainer.rectTransform.anchorMax = new Vector2(1, 1);
                CharacterContainer.rectTransform.anchoredPosition = new Vector3(-274.3f, 0, 0);
                break;

            case CharacterPosition.Middle:
                CharacterContainer.rectTransform.anchorMin = new Vector2(0.5f, 1);
                CharacterContainer.rectTransform.anchorMax = new Vector2(0.5f, 1);
                CharacterContainer.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                break;

            case CharacterPosition.None:
                CharacterContainer.gameObject.SetActive(false);
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
        foreach (Answer answer in CurrentConversation.Lines[LineNumber].LineAnswer)
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

        dialogAnswers.AnswerReactions(answerCase, npcDialogStarter);
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