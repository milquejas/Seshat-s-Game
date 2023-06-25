using UnityEngine;

[System.Serializable]
public struct CatStatuePuzzleQuestion
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



[CreateAssetMenu(fileName = "CatStatuePuzzle", menuName = "Puzzles/CatStatuePuzzleSO")]
public class CatStatuePuzzleSO : ScriptableObject
{
    public string HintMessage;

    [Header("0 is first answer")]
    public int[] CorrectAnswers;

    [Header("Needs 3 questions")]
    public CatStatuePuzzleQuestion[] Questions;
}
