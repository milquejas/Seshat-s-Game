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
    [SerializeField] private TaskDetailWindowUI TaskDetailComponent;

    [SerializeField] private GameObject playerControls;
    private List<TaskSO> tasksInMenu = new List<TaskSO>();
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
            TasksCanvas.SetActive(false);
            tasksMenuOpen = !tasksMenuOpen;
        }
            
        menuOpen = !menuOpen;

        MenuCanvas.SetActive(menuOpen);
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

        if (tasksMenuOpen) PoulateTasksMenu();
        TasksCanvas.SetActive(tasksMenuOpen);
        playerInteraction.DisablePlayerMovement(tasksMenuOpen);
    }

    private void PoulateTasksMenu()
    {
        TaskSO[] taskList = GameManager.GameManagerInstance.currentTaskList.Tasks;
        foreach (TaskSO task in taskList)
        {
            if (tasksInMenu.Contains(task)) return;

            tasksInMenu.Add(task);
            TaskMenuButton taskButton = Instantiate(TasksButtonPrefab, TaskButtonContainer, false);
            taskButton.InitializeTaskButton(task);
            taskButton.GetComponent<Button>().onClick.AddListener(delegate { TaskDetailComponent.ShowTaskDetails(task); });
        }
    }

    public void ShowTaskDetails(TaskSO task)
    {
        
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