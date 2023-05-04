using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
/*
* light starting reference:
* https://github.com/draffauf/unity-dialogue-system/blob/master/Assets/Scripts/SpeakerUIController.cs 
* Could add emotions to characters
* Questions and dialog branching is badly implemented. 
* Didn't think much how to make transitions for any dialog changes...
* People often pool UI objects by reparenting and then disabling them, which causes unnecessary dirtying.
* Solution: Disable the object first, then reparent it into the pool.
* 
* Event when dialog ends. 
*/

public class Dialog : MonoBehaviour
{
    [SerializeField] private Image CharacterPlacement;
    [SerializeField] private Image Portrait;
    [SerializeField] private TMP_Text SpeakerName;
    [SerializeField] private TMP_Text dialog;
    [SerializeField] private Image AnswerButtonPanel;
    [SerializeField] private GameObject AnswerButton;

    private List<GameObject> answerButtons = new List<GameObject>();

    public int lineNumber { get; set; }
    private bool answering;

    public ConversationSO CurrentConversation;
    private DialogAnswers dialogAnswers;

    [SerializeField] private bool StartTestConversation;

    [Header("hacked shitty solution, pick one the scene has")]
    [SerializeField] private TouchMovementAndInteraction playerControls;
    [SerializeField] private TouchAndMouseBehaviour playerControlsNoMovement;


    // EVENT!!!
    public event Action<ConversationSO> ConversationEnded;

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

    private void Start()
    {
        if (StartTestConversation)
            StartConversation(CurrentConversation);

        dialogAnswers = GetComponent<DialogAnswers>();
    }

    // Invoke dialog exit action
    public void ExitDialog()
    {
        ConversationEnded?.Invoke(CurrentConversation);

        if (playerControls != null)
        {
            playerControls.ControlDisabled = false;
        }
        if (playerControlsNoMovement != null)
        {
            playerControlsNoMovement.ControlDisabled = false;
        }

        gameObject.SetActive(false);
    }

    public void StartConversation(ConversationSO _conversation)
    {
        gameObject.SetActive(true);

        if(playerControls!= null) 
        {
            playerControls.ControlDisabled = true;
        }
        if (playerControlsNoMovement != null)
        {
            playerControlsNoMovement.ControlDisabled = true;
        }

        CurrentConversation = _conversation;
        lineNumber = 0;

        ShowDialog();
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
        /*
        switch (CurrentConversation.Lines[lineNumber].Position)
        {
            case CharacterPosition.Left:
                CharacterPlacement.gameObject.SetActive(true);
                CharacterPlacement.rectTransform.position = new Vector3(176f, CharacterPlacement.rectTransform.rect.position.y, CharacterPlacement.rectTransform.rect.position.y);
                CharacterPlacement.rectTransform.anchorMin = new Vector2(0, 1);
                CharacterPlacement.rectTransform.anchorMax = new Vector2(0, 1);
                break;

            case CharacterPosition.Right:
                CharacterPlacement.gameObject.SetActive(true);
                CharacterPlacement.rectTransform.position = new Vector3(0, CharacterPlacement.rectTransform.position.y, CharacterPlacement.rectTransform.position.z);
                CharacterPlacement.rectTransform.anchorMin = new Vector2(1, 1);
                CharacterPlacement.rectTransform.anchorMax = new Vector2(1, 1);
                CharacterPlacement.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                break;

            case CharacterPosition.Middle:
                CharacterPlacement.gameObject.SetActive(true);
                CharacterPlacement.rectTransform.position = new Vector3(0, CharacterPlacement.rectTransform.position.y, CharacterPlacement.rectTransform.position.z);
                CharacterPlacement.rectTransform.anchorMin = new Vector2(0.5f, 1);
                CharacterPlacement.rectTransform.anchorMax = new Vector2(0.5f, 1);
                CharacterPlacement.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                break;

            case CharacterPosition.None:
                CharacterPlacement.gameObject.SetActive(false);
                break;

            default:
                Debug.LogWarning("current conversation position switch hit default, oh no panic?!");
                break;
        }*/
    }

    // question behaviour starts here
    private void StartQuestion()
    {
        answering = true;
        // creates all answer buttons and sets their listeners
        // TODO optimize by pooling 4 buttons and adjusting text and listeners...
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
    
