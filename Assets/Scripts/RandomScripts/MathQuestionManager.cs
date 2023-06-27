using UnityEngine;

public class MathQuestionManager : MonoBehaviour
{
    public delegate void DisplayQuestionDelegate(MathQuestion mathQuestion);
    public static event DisplayQuestionDelegate OnDisplayQuestion;

    private MathQuestionGenerator questionGenerator;

    private void Start()
    {
        questionGenerator = new MathQuestionGenerator();
        DisplayMathQuestion();
    }

    private void DisplayMathQuestion()
    {
        // Kutsu questionGenerator-oliota generoimaan uusi matemaattinen tehtävä
        MathQuestion mathQuestion = questionGenerator.GenerateQuestionWithAnswers();

        // Lähetä tapahtuma (event) ja välitä tehtävän tiedot
        OnDisplayQuestion?.Invoke(mathQuestion);
    }
}
