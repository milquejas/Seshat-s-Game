using UnityEngine;
using UnityEngine.UI;

public class InteractWithStatue : MonoBehaviour
{
    public PatsasArvoitukset statueRiddles;
    public GameObject interactionPrompt; // A UI element that displays the interaction prompt
    public Button interactionButton; // A button that the player can click to interact with the statue
    public float interactionDistance = 2f; // Set the distance at which the player can interact with the statue

    private bool inInteractionRange = false; // Whether the player is within interaction range

    private void Update()
    {
        // Check if the player is within interaction range
        inInteractionRange = Vector2.Distance(CharacterMovement.instance.transform.position, transform.position) <= interactionDistance;

        // Show or hide the interaction prompt and button depending on whether the player is within interaction range
        interactionPrompt.SetActive(inInteractionRange);
        interactionButton.gameObject.SetActive(inInteractionRange);

        // If the player clicked and is within interaction range, start the riddles
        if (Input.GetMouseButtonDown(0) && inInteractionRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                statueRiddles.StartRiddles();
            }
        }
    }
}
