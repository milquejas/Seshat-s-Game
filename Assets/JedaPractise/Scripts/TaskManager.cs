using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public KeinuvaakaManager scaleManager;
    public List<Task> tasks;
    public int currentTaskIndex = 0;

    private void Start()
    {
        // Display the first task
        DisplayCurrentTask();
    }

    public void DisplayCurrentTask()
    {
        Task currentTask = tasks[currentTaskIndex];
        Debug.Log($"Task {currentTaskIndex + 1}: {currentTask.description}");
    }

    public void CheckTaskCompletion()
    {
        Task currentTask = tasks[currentTaskIndex];
        bool isTaskCompleted = false;

        // Check if the task is completed
        if (currentTask.cupTarget == CupTarget.LeftCup && scaleManager.CalculateSpecificWeightInCup(currentTask.itemType, scaleManager.leftCup) >= currentTask.targetWeight)
        {
            isTaskCompleted = true;
        }
        else if (currentTask.cupTarget == CupTarget.RightCup && scaleManager.CalculateSpecificWeightInCup(currentTask.itemType, scaleManager.rightCup) >= currentTask.targetWeight)
        {
            isTaskCompleted = true;
        }

        if (isTaskCompleted)
        {
            Debug.Log($"Task {currentTaskIndex + 1} completed!");
            currentTaskIndex++;

            if (currentTaskIndex < tasks.Count)
            {
                DisplayCurrentTask();
            }
            else
            {
                Debug.Log("All tasks completed!");
            }
        }
    }

}

[System.Serializable]
public class Task
{
    public string description;
    public string itemType;
    public float targetWeight;
    public CupTarget cupTarget;
}

public enum CupTarget
{
    LeftCup,
    RightCup
}
