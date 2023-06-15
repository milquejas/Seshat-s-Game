using UnityEngine;

[CreateAssetMenu(fileName = "New task", menuName = "Tasks/Task")]
public class TaskSO : ScriptableObject
{
    public string Title;
    [TextArea(3, 5)]
    public string Description;
    public int Progress;
    
    public bool Completed;
}

