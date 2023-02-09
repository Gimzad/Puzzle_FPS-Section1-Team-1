using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneControl
{
    static SceneControl()
    {
    }
    private SceneControl()
    {
    }
    public static SceneControl Instance { get; } = new SceneControl();

    public void LoadMainMenuScene()
    {
        //Load 1st scene in build order which should be made sure is the main menu scene. Can also do this by name once that is stuctrued but could be slower
        SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Additive);
    }

    public void LoadFirstLevel()
    {
        //Unload Main Menu Scene
        if (SceneManager.GetSceneByName("Main Menu").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Main Menu");
        }
        SceneManager.LoadSceneAsync("Test Level", LoadSceneMode.Additive);
    }
    public void SceneRestart_Game()
    {
        //If scence currently loaded is a level scene (player with enemies and objectives, which currently is only one test level)
        if (SceneManager.GetSceneByName("Test Level").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Test Level");
        }
        //Game manager will make call to BeginGame() again which loads main menu
    }
    public void SceneRestart_CurrentScene()
    {
        //Current Scene should be the one loaded after the paersistent one (1)
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
    }
}
