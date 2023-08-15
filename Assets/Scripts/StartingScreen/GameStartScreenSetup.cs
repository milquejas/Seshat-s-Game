using UnityEngine;

public class GameStartScreenSetup : MonoBehaviour
{
    float startTimer;

    private void Start()
    {
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    private void Update()
    {
        startTimer += Time.deltaTime;
        if(startTimer > 2f)
        {
            GameManager.GameManagerInstance.LoadScene("IsometricNewFlow");
        }
    }
}