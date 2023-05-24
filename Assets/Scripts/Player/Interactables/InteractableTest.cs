using UnityEngine;
using UnityEngine.Events;

public class InteractableTest : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public bool InRange { get; set; }

    public UnityEvent OnInteract;
    public void Interact()
    {
        Debug.Log($"interacted with{gameObject.name}");
        OnInteract.Invoke();
    }

    void OnMouseOver()
    {
        // Debug.Log($"Hovering over {gameObject.name}");
    }
}
