using TMPro;
using UnityEngine;


public class DisplayMathQuestion : MonoBehaviour
{
    [SerializeField]
    private TMP_Text firstNumberText;
    [SerializeField]
    private TMP_Text secondNumberText;
    [SerializeField]
    private TMP_Text operatorText;
    [SerializeField]
    private TMP_Text[] answerTexts;

    private MathQuestion currentQuestion;

    public void DisplayQuestion(MathQuestion mathQuestion)
    {
        currentQuestion = mathQuestion;

        firstNumberText.text = mathQuestion.FirstNumber.ToString();
        secondNumberText.text = mathQuestion.SecondNumber.ToString();
        operatorText.text = mathQuestion.OperatorType;

        // Näytä vastaukset
        for (int i = 0; i < mathQuestion.WrongAnswers.Count; i++)
        {
            answerTexts[i].text = mathQuestion.WrongAnswers[i].ToString();
        }

        // Aseta oikea vastaus satunnaiseen paikkaan vastauksista
        int correctAnswerIndex = UnityEngine.Random.Range(0, answerTexts.Length);
        answerTexts[correctAnswerIndex].text = mathQuestion.CorrectAnswer.ToString();
    }

    public void OnAnswerSelected(int selectedAnswerIndex)
    {
        int selectedAnswer = int.Parse(answerTexts[selectedAnswerIndex].text);
        if (selectedAnswer == currentQuestion.CorrectAnswer)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect!");
        }
    }
}

