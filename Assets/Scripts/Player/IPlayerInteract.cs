//All interactable objects need a script with this interface to be interactable
// some reference: https://www.youtube.com/watch?v=THmW4YolDok
using UnityEngine;

public interface IPlayerInteract
{
    public void DisablePlayerMovement(bool disable);
}
