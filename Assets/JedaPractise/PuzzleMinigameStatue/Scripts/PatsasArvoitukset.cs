using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PatsasArvoitukset : MonoBehaviour
{
    public Button[] question1Buttons;
    public Button[] question2Buttons;
    public Button[] question3Buttons;
    public TextMeshProUGUI question1;
    public TextMeshProUGUI question2;
    public TextMeshProUGUI question3;
    public Button checkAnswersButton;
    public Button quitButton; // Quit button
    public GameObject riddleUI;
    public TextMeshProUGUI hintMessage;

    private int[] selectedAnswers = new int[3];

    void Start()
    {
        riddleUI.SetActive(false);
        checkAnswersButton.onClick.AddListener(CheckAnswers);
        quitButton.onClick.AddListener(QuitRiddles); // Add quit action to quit button

        // Add listener for each answer button
        for (int i = 0; i < 3; i++)
        {
            int answerIndex = i; // Capture loop variable for closure
            question1Buttons[i].onClick.AddListener(() => SelectAnswer(0, answerIndex));
            question2Buttons[i].onClick.AddListener(() => SelectAnswer(1, answerIndex));
            question3Buttons[i].onClick.AddListener(() => SelectAnswer(2, answerIndex));
        }

        // Display the initial hint message
        hintMessage.text = "Solve the mystery of the statue!";
    }

    public void StartRiddles()
    {
        riddleUI.SetActive(true);
        CharacterMovement.instance.canMove = false;
    }

    public void QuitRiddles()
    {
        ResetRiddles();
    }

    public void CheckAnswers()
    {
        if (selectedAnswers[0] == 1 && selectedAnswers[1] == 0 && selectedAnswers[2] == 2) // Check if the selected answers are correct
        {
            // Hide all other UI elements
            foreach (var button in question1Buttons)
                button.gameObject.SetActive(false);
            foreach (var button in question2Buttons)
                button.gameObject.SetActive(false);
            foreach (var button in question3Buttons)
                button.gameObject.SetActive(false);
            question1.gameObject.SetActive(false);
            question2.gameObject.SetActive(false);
            question3.gameObject.SetActive(false);
            checkAnswersButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false); // Hide quit button

            // Show the hint message
            hintMessage.text = "Congratulations! You've solved all the puzzles. There's a secret hatch behind the statue!";

            CharacterMovement.instance.canMove = true; // Allow the player to move again
        }
        else
        {
            hintMessage.text = "At least one of your answers is incorrect. Please try again!";
        }
    }

   public void SelectAnswer(int questionIndex, int answerIndex)
{
    // Save the selected answer for the given question
    selectedAnswers[questionIndex] = answerIndex;

    // Change the color of the selected button and enable other buttons
    Button[][] allButtons = new Button[][] { question1Buttons, question2Buttons, question3Buttons };
    for (int i = 0; i < 3; i++)
    {
        var colors = allButtons[questionIndex][i].colors;
        allButtons[questionIndex][i].interactable = true; // Enable all buttons for new selection
        if (i == answerIndex)
        {
            colors.normalColor = Color.green;
        }
        else
        {
            colors.normalColor = Color.white;
        }
        allButtons[questionIndex][i].colors = colors;
    }

    // Disable the selected button after changing colors
    allButtons[questionIndex][answerIndex].interactable = false;
}


    public void ResetRiddles()
    {
        // Reset the riddle UI
        riddleUI.SetActive(false);

        // Show all other UI elements
        foreach (Button button in question1Buttons)
            button.gameObject.SetActive(true);
        foreach (Button button in question2Buttons)
            button.gameObject.SetActive(true);
        foreach (Button button in question3Buttons)
            button.gameObject.SetActive(true);
        question1.gameObject.SetActive(true);
        question2.gameObject.SetActive(true);
        question3.gameObject.SetActive(true);
        checkAnswersButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true); // Show quit button

        // Clear the selected answers
        for (int i = 0; i < selectedAnswers.Length; i++)
        {
            selectedAnswers[i] = -1;
        }

        // Reset the hint message
        hintMessage.text = "Solve the mystery of the statue!";

        CharacterMovement.instance.canMove = true; // Allow the player to move again
    }
}
