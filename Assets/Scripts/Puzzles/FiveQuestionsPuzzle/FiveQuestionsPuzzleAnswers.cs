using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FiveQuestionsPuzzleAnswers : MonoBehaviour
{

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI optionAText;
    public TextMeshProUGUI optionBText;
    public TextMeshProUGUI optionCText;

    public GameObject optionAButton;
    public GameObject optionBButton;
    public GameObject optionCButton;

    public FiveQuestionsPuzzleManager dialogueManager;

    private int playerScore;
    private int currentTaskIndex;
    private List<int> taskOrder;
    
    public FiveQuestionsSO[] AllPuzzles;

    private FiveQuestionsSO currentFiveQuestionsSO;

    void Start()
    {
        StartFiveQuestionsPuzzle(AllPuzzles[0]);
    }

    public void StartFiveQuestionsPuzzle(FiveQuestionsSO chosenPuzzle)
    {
        currentFiveQuestionsSO = chosenPuzzle;
        taskOrder = GenerateRandomTaskOrder();
        DisplayTask(taskOrder[currentTaskIndex]);
        playerScore = 0;

    }



    // Set the visibility of the question and answer buttons
    void SetQuestionAndButtonsVisibility(bool visible)
    {
        questionText.gameObject.SetActive(visible);
        optionAButton.SetActive(visible);
        optionBButton.SetActive(visible);
        optionCButton.SetActive(visible);
    }

    
    
    // Generates a random order for tasks
    List<int> GenerateRandomTaskOrder()
    {
        List<int> order = new List<int>();
        for (int i = 0; i < currentFiveQuestionsSO.question.Length; i++)
        {
            order.Add(i);
        }

        // Shuffle the task order
        for (int i = 0; i < currentFiveQuestionsSO.question.Length; i++)
        {
            int temp = order[i];
            int randomIndex = Random.Range(i, currentFiveQuestionsSO.question.Length);
            order[i] = order[randomIndex];
            order[randomIndex] = temp;
        }

        return order;
    }

    // Displays the task with the given task index
    void DisplayTask(int taskIndex)
    {
        FiveQuestionTask currentTask = currentFiveQuestionsSO.question[taskIndex];

        // Shuffle the options
        List<string> shuffledOptions = new List<string>(currentTask.options);
        for (int i = 0; i < shuffledOptions.Count; i++)
        {
            string temp = shuffledOptions[i];
            int randomIndex = Random.Range(i, shuffledOptions.Count);
            shuffledOptions[i] = shuffledOptions[randomIndex];
            shuffledOptions[randomIndex] = temp;
        }

        // Find the new index of the correct option
        int newCorrectOptionIndex = shuffledOptions.IndexOf(currentTask.options[currentTask.correctOptionIndex]);

        // Update the task with the shuffled options and new correct option index
        currentTask.options = shuffledOptions.ToArray();
        currentTask.correctOptionIndex = newCorrectOptionIndex;

        // Update the UI with the new task information
        questionText.text = currentTask.question;
        optionAText.text = currentTask.options[0];
        optionBText.text = currentTask.options[1];
        optionCText.text = currentTask.options[2];
    }

    // Checks if the selected option is correct
    public void CheckOption(int optionIndex)
    {
        if (currentFiveQuestionsSO.question[taskOrder[currentTaskIndex]].correctOptionIndex == optionIndex)
        {
            // Add points for the correct answer, but do not exceed the total number of questions
            playerScore = Mathf.Min(playerScore + 1, currentFiveQuestionsSO.question.Length); 
        }

        currentTaskIndex++;

        if (currentTaskIndex < currentFiveQuestionsSO.question.Length)
        {
            DisplayTask(taskOrder[currentTaskIndex]);
        }
        else
        {
            // Hide the question and buttons
            SetQuestionAndButtonsVisibility(false); 

            if (playerScore < 4)
            {
                dialogueManager.ResultLines = new string[] { $"You answered {playerScore}/5 correctly. Try again from the beginning." };
                dialogueManager.RestartQuest(() =>
                {
                    currentTaskIndex = 0; // Reset the task index
                    taskOrder = GenerateRandomTaskOrder(); // Generate a new random order
                    DisplayTask(taskOrder[currentTaskIndex]);
                    SetQuestionAndButtonsVisibility(true); // Show the question and buttons
                });
            }
            else
            {
                dialogueManager.ResultLines = new string[] { $"Congratulations! You answered {playerScore}/5 correctly!" };
                dialogueManager.RestartQuest(null);
                // At this point, you can add code to transition to the next event or scene
            }
        }
    }
}