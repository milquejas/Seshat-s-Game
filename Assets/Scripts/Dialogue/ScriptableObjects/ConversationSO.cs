using UnityEngine;

/*
 * What characters speak, 
 *
 * 
*/

[System.Serializable]
public struct Line
{
    public CharacterSO character;
    public CharacterPosition Position;
    public DialogLineType LineType;

    [TextArea(3, 5)]
    public string dialogueText;
    public Answer[] LineAnswer;
}

[System.Serializable]
public struct Answer
{
    [TextArea(1, 3)]
    public string AnswerText;
    [TextArea(1, 1)]
    public string AnswerSwitchCase;
}

public enum CharacterPosition
{
    Left,
    Right,
    Middle,
    None
}

public enum DialogLineType
{
    Dialog,
    Question,
    Puzzle
}

[CreateAssetMenu(fileName = "New conversation", menuName = "Conversation")]
public class ConversationSO : ScriptableObject
{
    public string ConversationName;
    public Line[] Lines;
}
