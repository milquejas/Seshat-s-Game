using UnityEngine;

public class InteractableTest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log($"interacted with{gameObject.name}");
    }

    void OnMouseOver()
    {
        Debug.Log($"Hovering over {gameObject.name}");
    }
}
