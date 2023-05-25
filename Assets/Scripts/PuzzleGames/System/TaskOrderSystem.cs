using System.Collections.Generic;
using UnityEngine;

public class TaskOrderSystem : MonoBehaviour
{
    public int taskCount;

    private List<int> taskOrder;

    public void GenerateTaskOrder()
    {
        taskOrder = new List<int>();
        for (int i = 0; i < taskCount; i++)
        {
            taskOrder.Add(i);
        }

        // Shuffle the task order
        for (int i = 0; i < taskCount; i++)
        {
            int temp = taskOrder[i];
            int randomIndex = Random.Range(i, taskCount);
            taskOrder[i] = taskOrder[randomIndex];
            taskOrder[randomIndex] = temp;
        }
    }

    public int GetNextTaskIndex(CurrentTaskIndexComponent currentTaskIndex)
    {
        currentTaskIndex.currentTaskIndex++;
        return taskOrder[currentTaskIndex.currentTaskIndex];
    }
}
