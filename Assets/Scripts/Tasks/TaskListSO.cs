using System.Collections.Generic;
using UnityEngine;

// keep a reference active in game manager to keep garbage collection off TaskSO

[CreateAssetMenu(fileName = "New task list", menuName = "Tasks/TaskList")]
public class TaskListSO : ScriptableObject
{
    public string Title;
    public TaskSO[] Tasks;
}