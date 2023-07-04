using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private GameObject MenuCanvas;

    [SerializeField] private GameObject playerControls;
    private IPlayerTouch playerInteraction;

    private bool menuOpen;

    private void Start()
    {
        playerInteraction = playerControls.GetComponent<IPlayerTouch>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
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

    public void ToggleMenu()
    {
        menuOpen = !menuOpen;
        
        MenuCanvas.SetActive(menuOpen);
        playerInteraction.DisablePlayerMovement(menuOpen);
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