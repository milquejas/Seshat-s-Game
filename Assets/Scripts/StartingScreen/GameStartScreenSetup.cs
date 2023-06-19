using UnityEngine;

public class GameStartScreenSetup : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
       GameManager.GameManagerInstance.LoadScene("IsometricMain");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
