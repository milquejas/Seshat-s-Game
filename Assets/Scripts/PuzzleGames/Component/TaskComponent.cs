using UnityEngine;

public struct TaskComponent
{
    public string question;
    public string[] options;
    public int correctOptionIndex;
}

public struct DialogueComponent
{
    public string[] dialogueLines;
}

public struct ScoreComponent
{
    public int playerScore;
}

public struct DisplayComponent
{
    public bool isVisible;
}

public struct CurrentTaskIndexComponent
{
    public int currentTaskIndex;
}
