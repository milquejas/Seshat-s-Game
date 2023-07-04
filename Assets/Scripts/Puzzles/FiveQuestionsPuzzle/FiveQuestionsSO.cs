using UnityEngine;


[CreateAssetMenu(fileName = "New Five Questions", menuName = "Puzzles/FiveQuestionsPuzzle")]

public class FiveQuestionsSO : ScriptableObject
{
    public FiveQuestionTask[] question;
}


[System.Serializable]
public class FiveQuestionTask
{
    [TextArea(3, 5)]
    public string question;
    public string[] options;
    public int correctOptionIndex;
}
