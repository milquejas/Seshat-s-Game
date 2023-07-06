using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FiveQuestionsPuzzleAnswers : MonoBehaviour
{
    public TouchMovementAndInteraction playerTouchMovement;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI optionAText;
    public TextMeshProUGUI optionBText;
    public TextMeshProUGUI optionCText;

    public GameObject fiveQuestionsPuzzleObject;

    public GameObject optionAButton;
    public GameObject optionBButton;
    public GameObject optionCButton;

    public GameObject exitButton;

    public FiveQuestionsPuzzleManager dialogueManager;

    private int playerScore;
    private int currentTaskIndex;
    private List<int> taskOrder;

    public FiveQuestionsSO[] AllPuzzles;

    private FiveQuestionsSO currentFiveQuestionsSO;

    public void StartFiveQuestionsPuzzle(FiveQuestionsSO chosenPuzzle)
    {
        currentFiveQuestionsSO = chosenPuzzle;
        playerScore = 0;
        currentTaskIndex = 0; // Reset the task index
        taskOrder = GenerateRandomTaskOrder(); // Generate a new random order

        DisplayTask(taskOrder[currentTaskIndex]);
        dialogueManager.resultPanel.SetActive(false);
        fiveQuestionsPuzzleObject.SetActive(true); // Make sure the FiveQuestionsPuzzle object is active
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
        List<int> order = new();
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
        List<string> shuffledOptions = new(currentTask.options);
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

        // Show the exit button when the answer buttons are visible
        exitButton.SetActive(true);
    }
    public void ExitPuzzle()
    {
        // Reset variables
        playerScore = 0;
        currentTaskIndex = 0;
        taskOrder.Clear();

        // Hide the entire FiveQuestionsPuzzle object
        fiveQuestionsPuzzleObject.SetActive(false);

        // Enable player movement
        playerTouchMovement.DisablePlayerMovement(false);

        // Show the question and buttons
        SetQuestionAndButtonsVisibility(true);

        // Generate a new random order
        taskOrder = GenerateRandomTaskOrder();

        // Display the first task
        DisplayTask(taskOrder[currentTaskIndex]);
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
            // Piilota exit-nappi
            exitButton.SetActive(false);

            if (playerScore < 4)
            {
                dialogueManager.resultLines = new string[] { $"You answered {playerScore}/5 correctly. Try again from the beginning." };
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
                dialogueManager.resultLines = new string[] { $"Congratulations! You answered {playerScore}/5 correctly!" };
                dialogueManager.RestartQuest(() =>
                {
                    ExitPuzzle(); // Call the exit method
                });
                // At this point, you can add code to transition to the next event or scene
            }
        }
    }


}