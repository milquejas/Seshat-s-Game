using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PatsasArvoitukset : MonoBehaviour
{
    public TMP_InputField answer1;
    public TMP_InputField answer2;
    public TMP_InputField answer3;
    public TextMeshProUGUI question1;
    public TextMeshProUGUI question2;
    public TextMeshProUGUI question3;
    public Button checkAnswersButton;
    public Button quitButton; // Quit button
    public GameObject riddleUI;
    public TextMeshProUGUI hintMessage;

    void Start()
    {
        riddleUI.SetActive(false);
        checkAnswersButton.onClick.AddListener(CheckAnswers);
        quitButton.onClick.AddListener(QuitRiddles); // Add quit action to quit button

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
        if (answer1.text == "32" && answer2.text == "64" && answer3.text == "640")
        {
            // Hide all other UI elements
            answer1.gameObject.SetActive(false);
            answer2.gameObject.SetActive(false);
            answer3.gameObject.SetActive(false);
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

    public void ResetRiddles()
    {
        // Reset the riddle UI
        riddleUI.SetActive(false);

        // Show all other UI elements
        answer1.gameObject.SetActive(true);
        answer2.gameObject.SetActive(true);
        answer3.gameObject.SetActive(true);
        question1.gameObject.SetActive(true);
        question2.gameObject.SetActive(true);
        question3.gameObject.SetActive(true);
        checkAnswersButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true); // Show quit button

        // Clear the answers
        answer1.text = "";
        answer2.text = "";
        answer3.text = "";

        // Reset the hint message
        hintMessage.text = "Solve the mystery of the statue!";

        CharacterMovement.instance.canMove = true; // Allow the player to move again
    }
}
