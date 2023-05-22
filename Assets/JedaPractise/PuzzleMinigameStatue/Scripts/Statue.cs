using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public GameObject interactionPrompt; // Vuorovaikutuspropti.
    public GameObject riddleWindow; // Arvoitusikkuna.
    public PlayerController playerController; // PlayerController viittaus.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactionPrompt.SetActive(false);
            playerController.UnfreezePlayer(); // Unfreeze player when they walk away from the statue.
        }
    }

    public void OnInteractButtonClicked()
    {
        interactionPrompt.SetActive(false);
        riddleWindow.SetActive(true);
        playerController.FreezePlayer(); // Freeze player when they interact with the statue.
    }
}
