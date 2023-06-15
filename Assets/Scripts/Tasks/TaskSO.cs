using UnityEngine;

[CreateAssetMenu(fileName = "New task", menuName = "Tasks/Task")]
public class TaskSO : ScriptableObject
{
    public string Title;
    [TextArea(3, 5)]
    public string Description;
    public int Progress;
    
    public bool Completed;

    private void OnDisable()
    {
        Progress = 0;
        Completed = false;
    }
    private void OnEnable()
    {
        Progress = 0;
        Completed = false;
    }
}