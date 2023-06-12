using UnityEngine;
using UnityEngine.Events;

public class RandomPuzzleNPC : MonoBehaviour, IInteractable
{
    [field: SerializeField] public bool InRange { get; set; }
    public UnityEvent StartDialog;

    public Transform Interact()
    {
        StartDialog.Invoke();
        return transform;
    }
}
