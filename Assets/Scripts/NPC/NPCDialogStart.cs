using System.Collections;
using UnityEngine;

public class NPCDialogStart : MonoBehaviour, IInteractable
{
    private bool _inRange;
    private bool conversationDisabled;
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
        dialogSystem.StartConversation(conversation);
        return transform;
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