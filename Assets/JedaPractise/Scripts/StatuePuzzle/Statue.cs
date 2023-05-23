using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Statue class manages interactions with the statue game object.
public class Statue : MonoBehaviour
{
    public GameObject interactionPrompt; // The interaction prompt game object.
    public GameObject riddleWindow; // The riddle window game object.
    public PlayerMovement playerMovement; // Reference to the PlayerController script.

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
            playerMovement.UnfreezePlayer();
        }
    }

    // OnInteractButtonClicked method is called when the interact button is clicked.
    public void OnInteractButtonClicked()
    {
        // Setting the interaction prompt game object to inactive.
        interactionPrompt.SetActive(false);
        // Setting the riddle window game object to active.
        riddleWindow.SetActive(true);
        // Calling the FreezePlayer method of the playerController to disable player movement.
        playerMovement.FreezePlayer();
    }
}
