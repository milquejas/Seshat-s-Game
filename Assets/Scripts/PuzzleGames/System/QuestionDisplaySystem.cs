using TMPro;
using UnityEngine;

public class QuestionDisplaySystem : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI optionAText;
    public TextMeshProUGUI optionBText;
    public TextMeshProUGUI optionCText;

    public void UpdateDisplay(TaskComponent task)
    {
        questionText.text = task.question;
        optionAText.text = task.options[0];
        optionBText.text = task.options[1];
        optionCText.text = task.options[2];
    }
}