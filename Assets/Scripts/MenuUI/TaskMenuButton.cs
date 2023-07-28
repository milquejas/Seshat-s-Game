using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskMenuButton : MonoBehaviour
{
    public TaskSO buttonTask;
    private TaskDetailWindowUI detailsWindow;

    [SerializeField] private TMP_Text taskTitleText;

    public void InitializeTaskButton(TaskDetailWindowUI details, TaskSO task)
    {
        buttonTask = task;
        taskTitleText.text = buttonTask.Title;
        detailsWindow = details;

        if (buttonTask.Completed)
        {
            taskTitleText.fontStyle = FontStyles.Strikethrough;
            taskTitleText.color = Color.gray;
        }
    }

    public void ButtonClicked()
    {
        detailsWindow.ShowTaskDetails(buttonTask);
    }
}
