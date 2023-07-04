using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MathQuess : MonoBehaviour
{
    [SerializeField]
    private TMP_Text firstNumber;
    [SerializeField]
    private TMP_Text secondNumber;
    [SerializeField]
    private TMP_Text answer1;
    [SerializeField]
    private TMP_Text answer2;
    [SerializeField]
    private TMP_Text operatorSign;
    [SerializeField]
    private TMP_Text rightOrWrong_Text;
    [SerializeField]
    private GameObject operators;
    [SerializeField]
    private GameObject canvas;
    [SerializeField] private Button answer1Button;
    [SerializeField] private Button answer2Button;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button quitButton;


    private int firstNumberInProblem;
    private int secondNumberInProblem;

    private int answerOne;
    private int answerTwo;
    private int displayRandomAnswer;

    private int randomAnswerPlacement;
    private int currentAnswer;

    private string currentOperator;

    // Vaikeutuva tehtävä range testit
    [SerializeField] private int minNumberRange = 1;
    [SerializeField] private int maxNumberRange = 10;
    [SerializeField] private int successRangeIncrease = 2;
    [SerializeField] private int failureRangeReduction = 1; 
    private int defaultMaxNumberRange;

    [SerializeField]
    private TouchMovementAndInteraction movement;

    private void Start()
    {
        defaultMaxNumberRange = maxNumberRange;
    }

    public void StartTotemQuest()
    {
        ResetRange();
        operators.SetActive(true);
        canvas.SetActive(false);
    }

    private void EndTotemQuest()
    {
        operators.SetActive(false);
        canvas.SetActive(false);
        movement.DisablePlayerMovement(false);
    }

    private void GenerateRandomNumbersPlusMinus()
    {
        firstNumberInProblem = Random.Range(minNumberRange, maxNumberRange);
        secondNumberInProblem = Random.Range(minNumberRange, maxNumberRange);
    }

    private void GenerateMultiplicationRandomNumbers()
    {
        
        

        firstNumberInProblem = Random.Range(1, 10);
        secondNumberInProblem = Random.Range(0, 10);
    }

    private void GenerateDivisionNumbers()
    {
        int a = Random.Range(1, 10);
        int b = Random.Range(1, 10);
        
        firstNumberInProblem = a * b;
        secondNumberInProblem = a;
        answerOne = b;
    }

    private void RandomAnswer()
    {
        // Generate a random number (0 or 1) to determine the order of the answers on the screen
        displayRandomAnswer = Random.Range(0, 2);

        /* 
          Create the wrong answer (answerTwo) by adding or subtracting a random value between 1 and 3
          from the correct answer (answerOne).
       */

        if (displayRandomAnswer == 0)
        {
            answerTwo = answerOne + Random.Range(1, 7);
        }
        else
        {
            answerTwo = answerOne - Random.Range(1, 7);
        }
    }

    public void DisplayMathProblem(string Buttontype)
    {
        /* 
        * Set the currentOperator based on the provided Buttontype parameter. 
        * Possible values for Buttontype are "+", "-", "*", or "/".
        * AnswerOne will always be the right answer, and the second answer will be wrong.
        */
        EnableButtons(true);
        currentOperator = Buttontype;
        canvas.SetActive(true);
        operators.SetActive(false);

        // Calculate the correct answer based on the current operator
        switch (currentOperator)
        {
            case "+":
                // Show the canvas and hide the operators game object
                GenerateRandomNumbersPlusMinus();
                operatorSign.text = currentOperator;
                answerOne = firstNumberInProblem + secondNumberInProblem;
                break;

            case "-":
                GenerateRandomNumbersPlusMinus();
                
                operatorSign.text = currentOperator;
                answerOne = firstNumberInProblem - secondNumberInProblem;
                break;

            case "*":
                GenerateMultiplicationRandomNumbers();

                operatorSign.text = currentOperator;
                answerOne = firstNumberInProblem * secondNumberInProblem;
                break;

            case "/":
                GenerateDivisionNumbers();
                operatorSign.text = currentOperator;
                break;
        }

        RandomAnswer();

        // Update the UI elements to display the numbers and answers
        firstNumber.text = "" + firstNumberInProblem;
        secondNumber.text = "" + secondNumberInProblem;

        // Determine the position of the correct answer and display both answers on the screen
        randomAnswerPlacement = Random.Range(0, 2);

        if (randomAnswerPlacement == 0)
        {
            answer1.text = "" + answerOne;
            answer2.text = "" + answerTwo;
            // currentAnswer is set to 0, indicating that answer1 is the correct answer.
            currentAnswer = 0;
        }
        else
        {
            answer1.text = "" + answerTwo;
            answer2.text = "" + answerOne;
            // currentAnswer is set to 1, indicating that answer2 is the correct answer.
            currentAnswer = 1;

        }
    }

    public void ButtonAnswer1()
    {
        EnableButtons(false);
        if (currentAnswer == 0)
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.blue;
            rightOrWrong_Text.text = ("Correct!");
            Invoke(nameof(TurnOffText), 1);
            CorrectAnswerPressed();
        }
        else
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.red;
            rightOrWrong_Text.text = ("Try Again!");
            Invoke(nameof(TurnOffText), 1);
            WrongAnswerPressed();
        }
    }

    public void ButtonAnswer2()
    {
        EnableButtons(false);
        if (currentAnswer == 1)
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.blue;
            rightOrWrong_Text.text = ("Corrent!");
            Invoke(nameof(TurnOffText), 1);
            CorrectAnswerPressed();
        }
        else
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.red;
            rightOrWrong_Text.text = ("Try Again!");
            Invoke(nameof(TurnOffText), 1);
            WrongAnswerPressed();
        }
    }

    private void EnableButtons(bool enable)
    {
        answer1Button.interactable = enable;
        answer2Button.interactable = enable;
        returnButton.interactable = enable;
        quitButton.interactable = enable;
    }

    private void CorrectAnswerPressed()
    {
        maxNumberRange += successRangeIncrease;
    }

    private void WrongAnswerPressed()
    {
        maxNumberRange -= failureRangeReduction;
    }

    private void ResetRange()
    {
        maxNumberRange = defaultMaxNumberRange;
    }

    // Tämä asettaa aina seuraavan tehtävän pelaajan valinnan jälkeen 
    public void TurnOffText()
    {
        if (rightOrWrong_Text != null)
        {
            rightOrWrong_Text.enabled = false;
            DisplayMathProblem(currentOperator);
        }
    }
}
