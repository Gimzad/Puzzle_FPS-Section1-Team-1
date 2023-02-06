using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Components")]
    [SerializeField]
    PlayerController playerController;
	[SerializeField]
	CameraControl playerCamera;

    [Header("Game State Variables")]
    [SerializeField]
    bool isPaused;

	//Bool to determine when a scene with the player in it has started (I.E. Not in the main menu or level selection.
	//This lets the script know it can start tracking game events like winning or losing.
	[SerializeField]
	bool playStarted;

    [Header("Event Tracking")]
    [SerializeField]
    int enemiesRemaining;

	#region Management Tools
	private MenuManager menuManagerInstance;
	private HUDManager hUDManagerInstance;
	private SceneControl sceneControlInstance;
	#endregion
	#region GM Access Methods
	public PlayerController PlayerController()
    {
        return playerController;
    }
    public bool IsPaused()
    {
        return isPaused;
    }
    public int EnemiesRemaining()
    {
        return enemiesRemaining;
    }
    #endregion
	private void Awake()
	{
		menuManagerInstance = GetComponent<MenuManager>();
		hUDManagerInstance = GetComponent<HUDManager>();
		sceneControlInstance = GetComponent<SceneControl>();
	}

	void Start()
	{
		Begin();
	}
    private void LateUpdate()
    {
        if (playStarted)
        {
			ManagePlayerTasks();
        }
    }

    private void ManagePlayerTasks()
    {
        //Track by highlighting active quest or event, remove or cross out when done, add new tasks as they appear.
    }

    void Begin()
	{
		isPaused = true;
		playStarted = false;
		//Deactivate any menus up from a possible last play
		menuManagerInstance.DeactivateAllMenus();
		menuManagerInstance.InitializeMenusText();

		sceneControlInstance.LoadMainMenuScene();

		//Set menu to default
		menuManagerInstance.AssertMenuTextFromPlayerPreferencesDefault();

		//Kind of a lazy way right now to reset the preferences to default as well as the menus.
		//Less computation can be achieved by simply calling a function in preferences that resets the variables without parsing from the menu values

		//Assign default to active preferences
		menuManagerInstance.AssertMenuTextToPlayerPreferences();

		menuManagerInstance.DisplayMainMenu();
	}

	public void InitializePlay()
	{
		menuManagerInstance.DeactivateAllMenus();
		sceneControlInstance.LoadFirstLevel();

	}
	void RestartGame()
	{
		//Call to scene control to handle unloading anything we are currently in
		sceneControlInstance.SceneRestart_Game();
		//This call loads the main menu scene and menus
		Begin();

		if (!Cursor.visible)
			Cursor.visible = true;

		Cursor.lockState =  CursorLockMode.Confined;
	}

	void RestartLevel()
    {
		//Placeholder function to restart a level without going all the way back to the main menu
    }
	//Should be called right before the player is dropped in and gains control of the player.
	//Script values should be assigned from preferences, controls should be enabled and cursor hidden
	public void SetupPlayerAndCamera()
	{
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		menuManagerInstance.AssertMenuTextFromPlayerPreferencesDefault();

		playerCamera = Camera.main.GetComponent<CameraControl>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	public void AssertPlayerPreferencesToScript()
	{
		//Will take the active values from Player Preferences and assign those settings to the variables used in the player controller script
	}
	void HandleInGameMenuInput()
	{
		if (menuManagerInstance.CanToggleGameMenu())
			if (Input.GetKeyDown(PlayerPreferences.Instance.PLAYERMENUKEY))
			{
				if (isPaused && menuManagerInstance.GameMenuIsUp())
				{
					//playerCamera.LockCamera = true;
					menuManagerInstance.DisplayGameMenu();
					isPaused = true;
					Time.timeScale = 0f;
					Cursor.lockState = CursorLockMode.Confined;
					Cursor.visible = true;

				}
				else if (!isPaused && !menuManagerInstance.GameMenuIsUp())
				{
					//playerCamera.LockCamera = false;
					menuManagerInstance.CloseGameMenu();
					isPaused = false;
					Time.timeScale = 1f;
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
				}
			}
	}
}
