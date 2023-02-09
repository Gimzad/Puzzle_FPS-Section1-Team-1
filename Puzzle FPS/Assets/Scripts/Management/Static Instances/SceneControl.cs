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
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void LoadFirstLevel()
    {
        //Unload Main Menu Scene
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneAt(1))
            SceneManager.UnloadSceneAsync(1);

        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        SceneManager.sceneLoaded += OnPlayLevelLoaded;
    }
    public void SceneRestart_Game()
    {
        //If scence currently loaded is a level scene (player with enemies and objectives, which currently is only one test level)
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneAt(2))
            SceneManager.UnloadSceneAsync(2);
    }
    public void SceneRestart_CurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnPlayLevelLoaded(Scene s, LoadSceneMode mode)
    {
        //If the level with the player actively in it is loaded
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded)
        {
            GameManager.Instance.SetupPlayerAndCamera();
        }
        else
        {
            return;
        }
    }
}
