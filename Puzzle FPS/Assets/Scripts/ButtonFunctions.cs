using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void Resume()
    {
        GameManager.Instance.ToggleGameMenu();
    }
    public void Restart()
    {
        GameManager.Instance.ToggleGameMenu();
        GameManager.Instance.RestartLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
