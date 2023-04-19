using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
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
    public Image AnswerButtonPanel;
    public GameObject AnswerButton;

    private List<GameObject> answerButtons = new List<GameObject>();
    [SerializeField] private ConversationList allConversations;

    [SerializeField] private TouchMovementAndInteraction playerControls;

    private int lineNumber;
    private bool answering;

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

    }

    public void StartConversation(Conversation _conversation)
    {
        gameObject.SetActive(true);
        playerControls.disableTouch = true;

        CurrentConversation = _conversation;
        lineNumber = 0;
        // transform.gameObject.SetActive(true);

        ShowDialog();
    }

    public void DialogClicked()
    {
        if (answering) return;

        lineNumber++;

        ShowDialog();
    }

    private void ShowDialog()
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
        switch (answerCase)
        {
            case "ExitCase":

                
                ExitDialog();
                break;

            case "TestCase":
                StartConversation(allConversations.ConversationSO.Find(x => x.ConversationName == "KakkaConversation"));
                break;

            default:
                Debug.LogWarning("GiveAnswer hit default, typo in switch/conversation answer?");
                break;
        }
    }

    // destroy buttons and empty list, remove listeners just in case too
    private void DestroyUsedButtons()
    {
        foreach (GameObject oldButton in answerButtons)
        {
            oldButton.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(oldButton);
        }
        answerButtons.Clear();
    }

    private void ExitDialog()
    {
        playerControls.disableTouch = false;
        gameObject.SetActive(false);
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
                Debug.LogWarning("current conversation switch hit default, oh no panic?!");
                break;
        }
    }
}
