using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void QuitGameFunction()
    {
        // Quit the game (only works in standalone builds)
        Application.Quit();
    }
}
