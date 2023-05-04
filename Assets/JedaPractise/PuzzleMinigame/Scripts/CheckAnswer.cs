using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckAnswer : MonoBehaviour
{
    public Task[] tasks;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI optionAText;
    public TextMeshProUGUI optionBText;
    public TextMeshProUGUI optionCText;

    public DialogueManager dialogueManager;

    private int currentTaskIndex;
    private List<int> taskOrder;

    [System.Serializable]
    public class Task
    {
        public string question;
        public string[] options;
        public int correctOptionIndex;
    }

    void Start()
    {
        taskOrder = GenerateRandomTaskOrder();
        DisplayTask(taskOrder[currentTaskIndex]);
    }

    // Generates a random order for tasks
    List<int> GenerateRandomTaskOrder()
    {
        List<int> order = new List<int>();
        for (int i = 0; i < tasks.Length; i++)
        {
            order.Add(i);
        }

        // Shuffle the task order
        for (int i = 0; i < tasks.Length; i++)
        {
            int temp = order[i];
            int randomIndex = Random.Range(i, tasks.Length);
            order[i] = order[randomIndex];
            order[randomIndex] = temp;
        }

        return order;
    }

    // Displays the task with the given task index
    void DisplayTask(int taskIndex)
    {
        Task currentTask = tasks[taskIndex];

        // Shuffle the options
        List<string> shuffledOptions = new List<string>(currentTask.options);
        for (int i = 0; i < shuffledOptions.Count; i++)
        {
            string temp = shuffledOptions[i];
            int randomIndex = Random.Range(i, shuffledOptions.Count);
            shuffledOptions[i] = shuffledOptions[randomIndex];
            shuffledOptions[randomIndex] = temp;
        }

        // Find the new index of the correct option
        int newCorrectOptionIndex = shuffledOptions.IndexOf(currentTask.options[currentTask.correctOptionIndex]);

        // Update the task with the shuffled options and new correct option index
        currentTask.options = shuffledOptions.ToArray();
        currentTask.correctOptionIndex = newCorrectOptionIndex;

        // Update the UI with the new task information
        questionText.text = currentTask.question;
        optionAText.text = currentTask.options[0];
        optionBText.text = currentTask.options[1];
        optionCText.text = currentTask.options[2];
    }

    // Checks if the selected option is correct
    public void CheckOption(int optionIndex)
    {
        if (tasks[taskOrder[currentTaskIndex]].correctOptionIndex == optionIndex)
        {
            dialogueManager.dialogueLines = new string[] { "Oikein! Siirrytään seuraavaan tehtävään." };
            dialogueManager.StartDialogue(() =>
            {
                currentTaskIndex++;
                if (currentTaskIndex < tasks.Length)
                {
                    DisplayTask(taskOrder[currentTaskIndex]);
                }
                else
                {
                    dialogueManager.dialogueLines = new string[] { "Onneksi olkoon! Olet suorittanut kaikki tehtävät!" };
                    dialogueManager.StartDialogue(null);
                }
            });
        }
        else
        {
            dialogueManager.dialogueLines = new string[] { "Väärin! Yritä uudelleen alusta." };
            dialogueManager.StartDialogue(() =>
            {
                currentTaskIndex = 0; // Reset the task index
                taskOrder = GenerateRandomTaskOrder(); // Generate a new random order
                DisplayTask(taskOrder[currentTaskIndex]);
            });
        }
    }

}
