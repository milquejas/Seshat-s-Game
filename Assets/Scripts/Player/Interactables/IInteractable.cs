//All interactable objects need a script with this interface to be interactable
// some reference: https://www.youtube.com/watch?v=THmW4YolDok
public interface IInteractable
{
    public bool InRange { get; set; }
    public void Interact();
}
