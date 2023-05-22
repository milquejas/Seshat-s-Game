using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatueRiddle : MonoBehaviour
{
    public List<Button> optionButtons; // List of all the option buttons for the riddle.
    public TMP_Text hintText; // Text component that will display hints or messages to the player.
    public PlayerController playerController; // Reference to the PlayerController script.
    private int[] correctAnswers; // Array storing the correct answers for each question.
    private int[] playerAnswers; // Array storing the player's selected answers.

    // The Start method is called when the script instance is being loaded.
    void Start()
    {
        int numberOfQuestions = optionButtons.Count / 3; // Calculate the number of questions based on the number of buttons (assuming 3 options per question).

        correctAnswers = new int[numberOfQuestions]; // Initialize the correctAnswers array.
        playerAnswers = new int[numberOfQuestions]; // Initialize the playerAnswers array.

        // Setting the correct answers for each question.
        correctAnswers[0] = 0; // For the first question, the correct answer is the first option.
        correctAnswers[1] = 2; // For the second question, the correct answer is the third option.
        correctAnswers[2] = 1; // For the third question, the correct answer is the second option.

        // Initialize the playerAnswers array with -1 values, indicating that the player hasn't selected an answer yet.
        for (int i = 0; i < numberOfQuestions; i++)
        {
            playerAnswers[i] = -1;
        }
    }

    // This method is called when an option button is clicked.
    public void OnOptionButtonClicked(int buttonIndex)
    {
        // Disable the clicked button and enable the other buttons for the same question.
        int questionIndex = buttonIndex / 3; // Calculate the question index based on the button index (assuming 3 options per question).
        int startIndex = questionIndex * 3; // Calculate the start index for the group of buttons that belong to the same question.
        for (int i = 0; i < 3; i++) // Iterate through the three options of the question.
        {
            if (i + startIndex == buttonIndex)
            {
                optionButtons[i + startIndex].interactable = false; // Disable the clicked button.
                playerAnswers[questionIndex] = i; // Store the selected option.
            }
            else
            {
                optionButtons[i + startIndex].interactable = true; // Enable the other buttons.
            }
        }
    }

    // This method is called when the "Check Answers" button is clicked.
    public void OnCheckAnswersButtonClicked()
    {
        // Check if the player's selected answers match the correct answers.
        int correctCount = 0;
        for (int i = 0; i < correctAnswers.Length; i++)
        {
            if (playerAnswers[i] == correctAnswers[i])
            {
                correctCount++; // Increase the count of correct answers.
            }
        }

        // If the player got all answers correct...
        if (correctCount == correctAnswers.Length)
        {
            hintText.text = "Congratulations! You've solved all the puzzles. There's a secret hatch behind the statue!";
        }
        else
        {
            hintText.text = "At least one of your answers is incorrect. Please try again!";

            // If some answers are incorrect, reset all answers and enable all buttons for another try.
            for (int i = 0; i < optionButtons.Count; i++)
            {
                playerAnswers[i / 3] = -1; // Reset the answer for this question.
                optionButtons[i].interactable = true; // Enable the button.
            }
        }
    }

    // This method is called when the "Quit" button is clicked.
    public void OnQuitButtonClicked()
    {
        gameObject.SetActive(false); // Hide the riddle UI.

        // Reset all answers and enable all buttons when quitting the riddle.
        for (int i = 0; i < optionButtons.Count; i++)
        {
            playerAnswers[i / 3] = -1; // Reset the answer for this question.
            optionButtons[i].interactable = true; // Enable the button.
        }

        hintText.text = "Solve the mystery of the statue!"; // Reset the hint text.

        playerController.UnfreezePlayer(); // Unfreeze player when they quit the riddle.
    }
}
