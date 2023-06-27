
using System.Collections.Generic;
using UnityEngine;

public class MathQuestion
{
    public int FirstNumber { get; private set; }
    public int SecondNumber { get; private set; }
    public string OperatorType { get; private set; }
    public int CorrectAnswer { get; private set; }
    public List<int> WrongAnswers { get; private set; }

    public MathQuestion(int firstNumber, int secondNumber, string operatorType, int correctAnswer, List<int> wrongAnswers)
    {
        FirstNumber = firstNumber;
        SecondNumber = secondNumber;
        OperatorType = operatorType;
        CorrectAnswer = correctAnswer;
        WrongAnswers = wrongAnswers;
    }
}

public class MathQuestionGenerator
{
    private readonly List<int> numberList;

    public MathQuestionGenerator()
    {
        numberList = new List<int>();
        for (int i = 1; i <= 100; i++)
        {
            numberList.Add(i);
        }
    }

    public MathQuestion GenerateQuestionWithAnswers()
    {
        int firstNumber = GetRandomNumber();
        int secondNumber = GetRandomNumber();
        string operatorType = GetRandomOperator();

        int correctAnswer = CalculateAnswer(firstNumber, secondNumber, operatorType);

        List<int> wrongAnswers = new()
        {
            GenerateWrongAnswer(correctAnswer),
            GenerateWrongAnswer(correctAnswer)
        };
        // Tämä vaihtoehtoinen tapa kirjoittaa sama asia. 
        /*List<int> wrongAnswers = new List<int>();
        wrongAnswers.Add(GenerateWrongAnswer(correctAnswer));
        wrongAnswers.Add(GenerateWrongAnswer(correctAnswer));*/

        MathQuestion mathQuestion = new MathQuestion(firstNumber, secondNumber, operatorType, 
            correctAnswer, wrongAnswers);
        return mathQuestion;
    }

    private int GetRandomNumber()
    {
        int randomIndex = UnityEngine.Random.Range(0, numberList.Count);
        int randomNumber = numberList[randomIndex];
        numberList.RemoveAt(randomIndex);
        return randomNumber;
    }

    private string GetRandomOperator()
    {
        string[] operators = { "+", "-", "*", "/" };
        int randomIndex = UnityEngine.Random.Range(0, operators.Length);
        return operators[randomIndex];
    }

    private int CalculateAnswer(int firstNumber, int secondNumber, string operatorType)
    {
        int answer = 0;
        switch (operatorType)
        {
            case "+":
                answer = firstNumber + secondNumber;
                break;
            case "-":
                answer = firstNumber - secondNumber;
                break;
            case "*":
                answer = firstNumber * secondNumber;
                break;
            case "/":
                answer = firstNumber / secondNumber;
                break;
        }
        return answer;
    }

    private int GenerateWrongAnswer(int correctAnswer)
    {
        int wrongAnswer = correctAnswer;
        while (wrongAnswer == correctAnswer)
        {
            wrongAnswer = Random.Range(correctAnswer - 10, correctAnswer + 10);
        }
        return wrongAnswer;
    }
}


