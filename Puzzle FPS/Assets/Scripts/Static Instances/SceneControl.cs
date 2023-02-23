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

    public void LoadLevelOne()
    {
        //Unload Main Menu Scene
        if (SceneManager.GetSceneByName("Main Menu").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Main Menu");
        }
        AsyncOperation control = SceneManager.LoadSceneAsync("Level_One", LoadSceneMode.Additive);
        control.completed += (sceneComplete) => {
            GameManager.Instance.FetchEvents();
            GameManager.Instance.LevelSetup();
        };
    }
    public void LoadLevelTwo()
    { 
        if (SceneManager.GetSceneByName("Level_One").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Level_One");
        }
        AsyncOperation control = SceneManager.LoadSceneAsync("Level_Two", LoadSceneMode.Additive);
        control.completed += (sceneComplete) => {
            GameManager.Instance.FetchEvents();
            GameManager.Instance.LevelSetup();
        };
    }
    public void SceneRestart_Game()
    {
        //If scence currently loaded is a level scene (player with enemies and objectives)
        if (!SceneManager.GetSceneByName("Main Menu").isLoaded)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).name);
        }
        //Game manager will make call to BeginGame() again which loads main menu
    }
    public void SceneRestart_CurrentScene()
    {
        //Current Scene should be the one loaded after the paersistent one (1)
        int currSceneIndex = SceneManager.GetSceneAt(1).buildIndex;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        SceneManager.LoadSceneAsync(currSceneIndex, LoadSceneMode.Additive);
    }
    public Scene GetCurrentScene()
    {
        return SceneManager.GetSceneAt(1);
    }
}
