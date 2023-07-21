using TMPro;
using UnityEngine;

public class TaskDetailWindowUI : MonoBehaviour
{
    [SerializeField] private TMP_Text taskTitleText;
    [SerializeField] private TMP_Text taskDescriptionText;
    public void ShowTaskDetails(TaskSO task)
    {
        taskTitleText.text = task.Title;
        taskDescriptionText.text = task.Description;
    }
}
