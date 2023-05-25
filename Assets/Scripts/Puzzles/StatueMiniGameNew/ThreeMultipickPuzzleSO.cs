using UnityEngine;

[System.Serializable]
public struct ThreeMultipickQuestion
{
    [TextArea(4, 5)]
    public string MultipickQuestionText;

    [TextArea(1, 1)]
    public string MultipickAnswer1;
    [TextArea(1, 1)]
    public string MultipickAnswer2;
    [TextArea(1, 1)]
    public string MultipickAnswer3;
}



[CreateAssetMenu(fileName = "ThreeMultipickQuestionnairePuzzle", menuName = "Puzzles/ThreeMultipickQuestionnairePuzzle")]
public class ThreeMultipickPuzzleSO : ScriptableObject
{
    public string HintMessage;

    [Header("0 is first answer")]
    public int[] CorrectAnswers;

    [Header("Needs 3 questions")]
    public ThreeMultipickQuestion[] Questions;
}
