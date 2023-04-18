using UnityEngine;
using UnityEngine.UI;

/*
 * What characters speak, 
 *
 * 
*/

[System.Serializable]
public struct Line
{
    public Character character;
    public CharacterPosition Position;
    public DialogLineType LineType;

    [TextArea(2, 5)]
    public string dialogueText;
    public string[] answerText;
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
public class Conversation : ScriptableObject
{
    public Line[] Lines;
}
