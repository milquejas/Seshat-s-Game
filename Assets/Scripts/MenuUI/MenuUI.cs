using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private GameObject MenuCanvas;

    [SerializeField] private GameObject TasksCanvas;
    [SerializeField] private TaskMenuButton TasksButtonPrefab;
    [SerializeField] private Transform TaskButtonContainer;
    [SerializeField] private TaskDetailWindowUI taskDetailWindow;

    [SerializeField] private GameObject playerControls;
    private List<TaskSO> tasksInMenu = new();
    private List<TaskMenuButton> taskButtonsInMenu = new();
    private IPlayerTouch playerInteraction;

    private bool menuOpen;
    private bool tasksMenuOpen;

    private void Start()
    {
        playerInteraction = playerControls.GetComponent<IPlayerTouch>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (tasksMenuOpen)
            {
                ToggleTasksMenu();
                return;
            }

            ToggleMainMenu();
        }
    }

    public void SetSFXLevel(float sliderValue)
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicLevel(float sliderValue)
    {
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void ToggleMainMenu()
    {
        if (tasksMenuOpen)
        {
            taskDetailWindow.gameObject.SetActive(false);
            TasksCanvas.SetActive(false);
            tasksMenuOpen = !tasksMenuOpen;
        }
            
        menuOpen = !menuOpen;

        MenuCanvas.SetActive(menuOpen);
        taskDetailWindow.gameObject.SetActive(false);
        playerInteraction.DisablePlayerMovement(menuOpen);
    }

    public void ToggleTasksMenu()
    {
        if (menuOpen)
        {
            MenuCanvas.SetActive(false);
            menuOpen = !menuOpen;
        }
            
        tasksMenuOpen = !tasksMenuOpen;

        if (tasksMenuOpen) PopulateTasksMenu();
        TasksCanvas.SetActive(tasksMenuOpen);
        playerInteraction.DisablePlayerMovement(tasksMenuOpen);
    }

    private void PopulateTasksMenu()
    {
        foreach (TaskMenuButton button in taskButtonsInMenu)
        {
            if (button.buttonTask.Completed)
            {
                button.InitializeTaskButton(taskDetailWindow, button.buttonTask);
            }
        }

        TaskSO[] taskList = GameManager.GameManagerInstance.currentTaskList.Tasks;
        foreach (TaskSO task in taskList)
        {
            if (!tasksInMenu.Contains(task))
            {
                tasksInMenu.Add(task);

                TaskMenuButton taskButton = Instantiate(TasksButtonPrefab, TaskButtonContainer, false);
                taskButton.InitializeTaskButton(taskDetailWindow, task);
                taskButtonsInMenu.Add(taskButton);
            }
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}