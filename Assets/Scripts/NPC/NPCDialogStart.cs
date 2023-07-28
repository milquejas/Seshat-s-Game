using System.Collections;
using UnityEngine;

public class NPCDialogStart : MonoBehaviour, IInteractable
{
    private bool _inRange;


    [SerializeField] private bool conversationDisabled;
    [SerializeField] private bool pickPuzzleProgress;
    [SerializeField] private int taskProgressionPuzzle;

    private IEnumerator questionmarkAnimationCoroutine;
    public bool InRange
    {
        get => _inRange;

        set
        {
            _inRange = value;
            RangeChanged();
        }
    }

    [Header("Place conversationSO here")]
    [SerializeField] private ConversationSO conversation;
    [SerializeField] private ConversationSO TaskCompleteConversation;
    [SerializeField] private TaskSO npcTask;

    [Header("Child questionmarks animation variables")]
    [SerializeField] private RectTransform questionmark;
    [SerializeField] private float questionmarkAnimationSpeed = 0.005f;
    [SerializeField] private float questionmarkAnimationSize = 0.3f;
    private Dialog dialogSystem;

    private void Start()
    {
        dialogSystem = FindObjectOfType<Dialog>();
        questionmarkAnimationCoroutine = QuestionmarkAnimation();
    }

    public Transform Interact()
    {
        if (conversationDisabled) return transform;

        if (npcTask.Completed)
        {
            dialogSystem.StartConversation(TaskCompleteConversation, this);
            return transform;
        }
        
        if (pickPuzzleProgress)
        {
            GameManager.GameManagerInstance.CurrentPuzzleIndex = taskProgressionPuzzle;
        }

        dialogSystem.StartConversation(conversation, this);
        return transform;
    }

    public void TaskCompleted()
    {
        conversationDisabled = true;
        npcTask.Completed = true;

        StopCoroutine(questionmarkAnimationCoroutine);
        questionmark.gameObject.SetActive(false);
    }

    public void ChangeConversationAndTask(ConversationSO newConversation, ConversationSO newTaskDoneConversation, TaskSO newTask)
    {
        conversation = newConversation;
        TaskCompleteConversation = newTaskDoneConversation;
        npcTask = newTask;
    }

    // Animations here
    private void RangeChanged()
    {
        if (conversationDisabled)
        {
            return;
        }

        if (_inRange)
        {
            questionmark.gameObject.SetActive(true);
            StartCoroutine(questionmarkAnimationCoroutine);
        }

        else
        {
            StopCoroutine(questionmarkAnimationCoroutine);
            questionmark.gameObject.SetActive(false);
        }
    }

    private IEnumerator QuestionmarkAnimation()
    {
        while (true)
        {
            if(questionmark.localScale.x <= 1 - questionmarkAnimationSize)
            {
                questionmarkAnimationSpeed *= -1;
            }
            if (questionmark.localScale.x >= 1 + questionmarkAnimationSize)
            {
                questionmarkAnimationSpeed *= -1;
            }

            questionmark.localScale += new Vector3(questionmarkAnimationSpeed, questionmarkAnimationSpeed, questionmarkAnimationSpeed);
            yield return null;
        }
    }
}