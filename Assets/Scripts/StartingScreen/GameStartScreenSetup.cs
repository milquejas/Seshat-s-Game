using UnityEngine;

public class GameStartScreenSetup : MonoBehaviour
{
    void Start()
    {
       GameManager.GameManagerInstance.LoadScene("IsometricNewFlow");
    }
}