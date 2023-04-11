using UnityEngine;

public class InteractableTest : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public bool inRange { get; set; }
    public void Interact()
    {
        Debug.Log($"interacted with{gameObject.name}");
    }

    void OnMouseOver()
    {
        // Debug.Log($"Hovering over {gameObject.name}");
    }
}
