using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField]
    private List<int> easyMathList = new();
    [SerializeField]
    private List<int> mediumMathList = new();
    [SerializeField]
    private List<int> hardMathList = new();
    [SerializeField]
    private List<int> selectedList;

    private bool isListPopulated = false;


    private int randomFirstNumber;
    private int randomSecondNumber;

    private int firstNumberInProblem;
    private int secondNumberInProblem;

    private int answerOne;
    private int answerTwo;
    private int displayRandomAnswer;

    private int randomAnswerPlacement;
    private int currentAnswer;

    private string currentOperator;

    [SerializeField]
    private TouchMovementAndInteraction movement;

    public void StartTotemQuest()
    {
        //for (int i = 1; i <= 100; i++)
        //{
        //    easyMathList.Add(i);
        //}
        DisplayMathProblem("");
        canvas.SetActive(false);
    }

    private void EndTotemQuest()
    {
        operators.SetActive(false);
        canvas.SetActive(false);
        movement.DisablePlayerMovement(false);
    }

    private void PopulateList(List<int> list, int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            list.Add(i);
        }
    }

    private void ListOfDifficulty()
    {
        if (!isListPopulated)
        {
            PopulateList(easyMathList, 1, 10);
            PopulateList(mediumMathList, 1, 20);
            PopulateList(hardMathList, 1, 100);
            isListPopulated = true;
        }
    }

    private void ChooseRandomList()
    {
        // Päätä, käytetäänkö easyMathList, mediumMathList vai hardMathList
        // Voit asettaa selectedListin haluamaksesi
        // Voit vaihtaa tämän mediumMathListiin tai hardMathListiin tarvittaessa
        selectedList = easyMathList;
        selectedList = mediumMathList;
        selectedList = hardMathList;
    }

    private void ClearList()
    {
        easyMathList.Clear();
        mediumMathList.Clear();
        hardMathList.Clear();
    }

    private void GenerateRandomNumbers()
    {
        // Generate a random number as the first and second numbers using the easyMathList size as the upper limit
        randomFirstNumber = Random.Range(0, selectedList.Count);
        randomSecondNumber = Random.Range(0, selectedList.Count);

        // Store the randomly generated numbers as the first and second numbers in the problem
        firstNumberInProblem = randomFirstNumber;
        secondNumberInProblem = randomSecondNumber;
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
            answerTwo = answerOne + Random.Range(1, 4);
        }
        else
        {
            answerTwo = answerOne - Random.Range(1, 4);
        }
        Debug.Log("random answer");
    }

    // Create a dictionary to map operators to their corresponding operations

    public void DisplayMathProblem(string Buttontype)
    {
        operators.SetActive(true);

        ListOfDifficulty();
        GenerateRandomNumbers();

        /* 
        * Set the currentOperator based on the provided Buttontype parameter. 
        * Possible values for Buttontype are "+", "-", "*", or "/".
        * AnswerOne will always be the right answer, and the second answer will be wrong.
        */
        currentOperator = Buttontype;

        // Calculate the correct answer based on the current operator
        switch (currentOperator)
        {

            case "+":
                // Show the canvas and hide the operators game object
                canvas.SetActive(true);
                operators.SetActive(false);
                selectedList = easyMathList;
                operatorSign.text = currentOperator;
                answerOne = firstNumberInProblem + secondNumberInProblem;
                RandomAnswer();
                break;

            case "-":
                canvas.SetActive(true);
                operators.SetActive(false);
                selectedList = mediumMathList; 
                operatorSign.text = currentOperator;
                answerOne = firstNumberInProblem - secondNumberInProblem;
                RandomAnswer();
                break;

            case "*":
                canvas.SetActive(true);
                operators.SetActive(false);
                operatorSign.text = currentOperator;
                answerOne = firstNumberInProblem * secondNumberInProblem;
                RandomAnswer();
                break;

            case "/":
                canvas.SetActive(true);
                operators.SetActive(false);
                // Check for division by zero
                if (secondNumberInProblem == 0)
                {
                    secondNumberInProblem = 1;
                }
                operatorSign.text = currentOperator;
                // Calculate the correct answer for the division
                answerOne = firstNumberInProblem / secondNumberInProblem;

                // Check for the remainder in division
                if (firstNumberInProblem % secondNumberInProblem != 0)
                {
                    // If there is a remainder, call the method recursively to generate a new problem
                    DisplayMathProblem(currentOperator);
                    return;
                }
                RandomAnswer();
                break;
        }

        //RandomAnswer();

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
        if (currentAnswer == 0)
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.blue;
            rightOrWrong_Text.text = ("Correct!");
            Invoke(nameof(TurnOffText), 1);
        }
        else
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.red;
            rightOrWrong_Text.text = ("Try Again!");
            Invoke(nameof(TurnOffText), 1);
        }
    }
    public void ButtonAnswer2()
    {
        if (currentAnswer == 1)
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.blue;
            rightOrWrong_Text.text = ("Corrent!");
            Invoke(nameof(TurnOffText), 1);
        }
        else
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.red;
            rightOrWrong_Text.text = ("Try Again!");
            Invoke(nameof(TurnOffText), 1);
        }
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
