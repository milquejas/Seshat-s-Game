using TMPro;
using UnityEngine;

public class TaskMenuButton : MonoBehaviour
{
    private TaskSO buttonTask;

    [SerializeField] private TMP_Text taskTitleText;

    public void InitializeTaskButton(TaskSO task)
    {
        buttonTask = task;
        taskTitleText.text = buttonTask.Title;

        if (buttonTask.Completed)
        {
            taskTitleText.fontStyle = FontStyles.Strikethrough;
            taskTitleText.color = Color.gray;
        }
    }
}
