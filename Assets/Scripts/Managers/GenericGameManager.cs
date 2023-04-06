using UnityEngine;

/*
 * Initialization things here for now
*/

public class GenericGameManager : MonoBehaviour
{

    void Start()
    {
        // en tii‰ miten t‰n tekee static classissa ittess‰‰ pelin alussa ni t‰‰ll‰ vaan...
        InteractSystem.InitContactFilters();
    }

}
