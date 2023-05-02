using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Addition : MonoBehaviour
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
    private List<int> easyMathList = new();

    [SerializeField]
    private int randomFirstNumber;
    [SerializeField]
    private int randomSecondNumber;

    private int firstNumberInProblem;
    private int secondNumberInProblem;

    private int answerOne;
    private int answerTwo;
    private int displayRandomAnswer;

    private int randomAnswerPlacement;
    private int currentAnswer;

    private string currentOperator;


    //void Awake()
    //{
    //    // Get references to the TMP_Text components
    //    firstNumber = FindObjectOfType<TMP_Text>();
    //    secondNumber = FindObjectOfType<TMP_Text>();
    //    answer1 = FindObjectOfType<TMP_Text>();
    //    answer2 = FindObjectOfType<TMP_Text>();
    //    operatorSign = FindObjectOfType<TMP_Text>();
    //    rightOrWrong_Text = FindObjectOfType<TMP_Text>();
    //}

    private void Start()
    {
        for (int i = 1; i <= 100; i++)
        {
            easyMathList.Add(i);
        }
        DisplayMathProblem("+");
    }

    public void DisplayMathProblem(string Buttontype)
    {
        // Generate a random number as the first and second numbers
        randomFirstNumber = Random.Range(0, easyMathList.Count + 1);
        randomSecondNumber = Random.Range(0, easyMathList.Count + 1);

        // Assing your first and second number
        firstNumberInProblem = randomFirstNumber;
        secondNumberInProblem = randomSecondNumber;

        /* 
         * This is where you can enter either addition, subtraction, multiplication or division        
         AnswerOne is allways the right answer, second is wrong.
        */
        currentOperator = Buttontype;

        // Calculate the correct answer based on the current operator
        switch (currentOperator)
        {
            case "+":
                operatorSign.text = currentOperator;
                answerOne = firstNumberInProblem + secondNumberInProblem;
                break;           
        }

        //answerOne = firstNumberInProblem - secondNumberInProblem;
        displayRandomAnswer = Random.Range(0, 2);

        // Tässä luodaan väärä vastaus ja annetaan sille arvoksi 1-4 enemmän tai vähemmän,
        // mitä oikea vastaus olisi
        if (displayRandomAnswer == 0)
        {
            answerTwo = answerOne + Random.Range(1, 4);
        }
        else
        {
            answerTwo = answerOne - Random.Range(1, 4);
        }

        firstNumber.text = "" + firstNumberInProblem;
        secondNumber.text = "" + secondNumberInProblem;
        randomAnswerPlacement = Random.Range(0, 2);

        if (randomAnswerPlacement == 0)
        {
            answer1.text = "" + answerOne;
            answer2.text = "" + answerTwo;
            currentAnswer = 0;
        }
        else
        {
            answer1.text = "" + answerTwo;
            answer2.text = "" + answerOne;
            currentAnswer = 1;

        }
    }
    public void ButtonAnswer1()
    {
        if (currentAnswer == 0)
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.green;
            rightOrWrong_Text.text = ("Correct!");
            Invoke("TurnOffText", 1);
        }
        else
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.red;
            rightOrWrong_Text.text = ("Try Again!");
            Invoke("TurnOffText", 1);
        }
    }
    public void ButtonAnswer2()
    {
        if (currentAnswer == 1)
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.green;
            rightOrWrong_Text.text = ("Corrent!");
            Invoke("TurnOffText", 1);
        }
        else
        {
            rightOrWrong_Text.enabled = true;
            rightOrWrong_Text.color = Color.red;
            rightOrWrong_Text.text = ("Try Again!");
            Invoke("TurnOffText", 1);
        }
    }
    // Tämä void asettaa aina seuraavan tehtävän pelaajan valinnan jälkeen 
    public void TurnOffText()
    {
        if (rightOrWrong_Text != null)
        {
            rightOrWrong_Text.enabled = false;
            DisplayMathProblem(currentOperator);
        }
    }
}
