using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// The ThreeMultipickPuzzle class manages the riddle game logic.


public class CatStatuePuzzle : MonoBehaviour
{
    [SerializeField] private List<Button> optionButtons; // List of all the option buttons for the riddle.

    [SerializeField] private TouchMovementAndInteraction playerControls; // Reference to the PlayerController script.
    public CatStatuePuzzleSO[] AllPuzzles;
    
    private int[] playerAnswers; // Array storing the player's selected answers.
    private int[] correctAnswers;
    [SerializeField] private TMP_Text hintMessage, question1, question2, question3, question1Answer1, question1Answer2, question1Answer3, question2Answer1,
        question2Answer2, question2Answer3, question3Answer1, question3Answer2, question3Answer3;

    // Array storing the correct answers for each question.

    void Start()
    {

        int numberOfQuestions = optionButtons.Count / 3; // Calculate the number of questions based on the number of buttons (assuming 3 options per question).

        playerAnswers = new int[numberOfQuestions]; // Initialize the playerAnswers array.

        // Initialize the playerAnswers array with -1 values, indicating that the player hasn't selected an answer yet.
        for (int i = 0; i < numberOfQuestions; i++)
        {
            playerAnswers[i] = -1;
        }
    }

    public void InitializeThreeMultipickPuzzle(CatStatuePuzzleSO givenPuzzle)
    {
        correctAnswers = givenPuzzle.CorrectAnswers;
        hintMessage.text = givenPuzzle.HintMessage;
        question1.text = givenPuzzle.Questions[0].MultipickQuestionText;
        question2.text = givenPuzzle.Questions[1].MultipickQuestionText;
        question3.text = givenPuzzle.Questions[2].MultipickQuestionText;
        question1Answer1.text = givenPuzzle.Questions[0].MultipickAnswer1;
        question1Answer2.text = givenPuzzle.Questions[0].MultipickAnswer2;
        question1Answer3.text = givenPuzzle.Questions[0].MultipickAnswer3;
        question2Answer1.text = givenPuzzle.Questions[1].MultipickAnswer1;
        question2Answer2.text = givenPuzzle.Questions[1].MultipickAnswer2;
        question2Answer3.text = givenPuzzle.Questions[1].MultipickAnswer3;
        question3Answer1.text = givenPuzzle.Questions[2].MultipickAnswer1;
        question3Answer2.text = givenPuzzle.Questions[2].MultipickAnswer2;
        question3Answer3.text = givenPuzzle.Questions[2].MultipickAnswer3;
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
            hintMessage.text = "Congratulations! You've solved all the puzzles.";
            OnQuitButtonClicked();
            // TODO start coruoutine -> pari sekkaa jotain animaatiocrappia -> se coroutine callaa OnQuitButtonClicked
        }
        else
        {
            hintMessage.text = "At least one of your answers is incorrect. Please try again!";

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
        // Reset all answers and enable all buttons when quitting the riddle.
        for (int i = 0; i < optionButtons.Count; i++)
        {
            playerAnswers[i / 3] = -1; // Reset the answer for this question.
            optionButtons[i].interactable = true; // Enable the button.
        }

        hintMessage.text = "Solve the mystery of the statue!"; // Reset the hint text.

        gameObject.SetActive(false); // Hide the riddle UI.

        playerControls.DisablePlayerMovement(false); // Unfreeze player when they quit the riddle.
    }
}
