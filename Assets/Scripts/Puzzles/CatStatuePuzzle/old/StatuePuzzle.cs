using System;
using UnityEngine;

// The Statue class manages interactions with the statue game object.
public class StatuePuzzle : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactionPrompt; // The interaction prompt game object.
    [SerializeField] private GameObject riddleWindow; // The riddle window game object.
    [SerializeField] private TouchMovementAndInteraction playerMovement; // Reference to the PlayerController script.

    [SerializeField] private bool inRange;

    public bool InRange
    {
        get => inRange;

        set 
        { 
            inRange = value;
            TogglePuzzleWindow();
        }
    }

    private void TogglePuzzleWindow()
    {
        if (interactionPrompt.activeSelf)
        {
            interactionPrompt.SetActive(false);
        }
        else
        {
            interactionPrompt.SetActive(true);
        }
    }

    public Transform Interact()
    {
        // if riddle complete, start different dialog or something else
        riddleWindow.SetActive(true);
        DisablePlayerMovement(true);
        return transform;
    }

    // OnTriggerEnter2D method is called when the Collider2D other enters the trigger.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checking if the game object of the collider has the tag "Player".
        if (collision.gameObject.CompareTag("Player"))
        {
            // If true, setting the interaction prompt game object to active.
            interactionPrompt.SetActive(true);
        }
    }

    // OnTriggerExit2D method is called when the Collider2D other exits the trigger.
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Checking if the game object of the collider has the tag "Player".
        if (collision.gameObject.CompareTag("Player"))
        {
            // If true, setting the interaction prompt game object to inactive.
            interactionPrompt.SetActive(false);
            // Calling the UnfreezePlayer method of the playerController to enable player movement.
            DisablePlayerMovement(false);
        }
    }

    // OnInteractButtonClicked method is called when the interact button is clicked.
    public void OnInteractButtonClicked()
    {
        // Setting the interaction prompt game object to inactive.
        interactionPrompt.SetActive(false);
        // Setting the riddle window game object to active.

    }

    public void DisablePlayerMovement(bool toggle) 
    {
        playerMovement.DisablePlayerMovement(toggle);
    }
}
