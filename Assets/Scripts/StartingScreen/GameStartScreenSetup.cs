using UnityEngine;

public class GameStartScreenSetup : MonoBehaviour
{
    float startTimer;

    private void Update()
    {
        startTimer += Time.deltaTime;
        if(startTimer > 2f)
        {
            GameManager.GameManagerInstance.LoadScene("IsometricNewFlow");
        }
    }
}