using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathProblem : MonoBehaviour
{

    public Text firstNumber;
    public Text secondNumber;
    public Text answer1;
    public Text answer2;
    public Text operatorSign;
    public Text rightOrWrong_Text;
    public List<int> easyMathList = new List<int>();

    public int randomFirstNumber;
    public int randomSecondNumber;

    private int firstNumberInProblem;
    private int secondNumberInProblem;

    private int answerOne;
    private int answerTwo;
    private int displayRandomAnswer;
    
    private int randomAnswerPlacement;
    private int currentAnswer;
    
    private string currentOperator;

    

    private void Start()
    {
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
            case "-":
                operatorSign.text = currentOperator;
                answerOne = firstNumberInProblem - secondNumberInProblem;
                break;
            case "*":
                operatorSign.text = currentOperator;
                answerOne = firstNumberInProblem * secondNumberInProblem;
                break;
            case "/":
                if(firstNumberInProblem == 0)
                {
                    firstNumberInProblem++;
                }
                if(secondNumberInProblem == 0)
                {
                    secondNumberInProblem++;
                }
                operatorSign.text = currentOperator;
                answerOne = firstNumberInProblem / secondNumberInProblem;                
                break;
        }

        //answerOne = firstNumberInProblem - secondNumberInProblem;
        displayRandomAnswer = Random.Range(0, 2);

        // T�ss� luodaan v��r� vastaus ja annetaan sille arvoksi 1-4 enemm�n tai v�hemm�n,
        // mit� oikea vastaus olisi
        if ( displayRandomAnswer == 0)
        {
            answerTwo = answerOne + Random.Range(1, 5);
        }
        else
        {
            answerTwo = answerOne - Random.Range(1, 5);
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
        if ( currentAnswer == 0)
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
    // T�m� void asettaa aina seuraavan teht�v�n pelaajan valinnan j�lkeen 
    public void TurnOffText()
    {
        if(rightOrWrong_Text != null)
        {
            rightOrWrong_Text.enabled = false;
            DisplayMathProblem(currentOperator);
        }
    }

    
}
