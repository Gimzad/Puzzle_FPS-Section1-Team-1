using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    #region Game Menu Buttons
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
    public void QuitToMenu()
    {
        GameManager.Instance.RestartGame();
    }
    #endregion

    #region Core Menu Buttons
    public void StartLevel()
    {
        //Get rid of main menu camera
        Destroy(Camera.main.gameObject);
        GameManager.Instance.InitializePlay();
    }
    public void OpenSettingsPanel()
    {
        MenuManager.Instance.DisplayPlayerSettingsMenu();
    }
    public void OpenCreditsPanel()
    {
        MenuManager.Instance.DisplayCreditsMenu();
    }
    #endregion

}
