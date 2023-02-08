using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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
	private HUDManager hUDManagerInstance;
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
		hUDManagerInstance = GetComponent<HUDManager>();
	}

	void Start()
	{
		//Begin();
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
		MenuManager.Instance.DeactivateAllMenus();
		MenuManager.Instance.InitializeMenusText();

		SceneControl.Instance.LoadMainMenuScene();

		//Set menu to default
		MenuManager.Instance.AssertMenuTextFromPlayerPreferencesDefault();

		//Kind of a lazy way right now to reset the preferences to default as well as the menus.
		//Less computation can be achieved by simply calling a function in preferences that resets the variables without parsing from the menu values

		//Assign default to active preferences
		MenuManager.Instance.AssertMenuTextToPlayerPreferences();

		MenuManager.Instance.DisplayMainMenu();
	}

	public void InitializePlay()
	{
		MenuManager.Instance.DeactivateAllMenus();
		SceneControl.Instance.LoadFirstLevel();

	}
	void RestartGame()
	{
		//Call to scene control to handle unloading anything we are currently in
		SceneControl.Instance.SceneRestart_Game();
		//This call loads the main menu scene and menus
		Begin();

		if (!Cursor.visible)
			Cursor.visible = true;

		Cursor.lockState =  CursorLockMode.Confined;
	}

	public void RestartLevel()
    {
		//Placeholder function to restart a level without going all the way back to the main menu
    }
	//Should be called right before the player is dropped in and gains control of the player.
	//Script values should be assigned from preferences, controls should be enabled and cursor hidden
	public void SetupPlayerAndCamera()
	{
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		MenuManager.Instance.AssertMenuTextFromPlayerPreferencesDefault();

		playerCamera = Camera.main.GetComponent<CameraControl>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	public void AssertPlayerPreferencesToScript()
	{
		//Will take the active values from Player Preferences and assign those settings to the variables used in the player controller script
	}

	public void ToggleGameMenu()
    {
		if (isPaused && MenuManager.Instance.GameMenuIsUp())
		{
			//playerCamera.LockCamera = true;
			MenuManager.Instance.DisplayGameMenu();
			isPaused = true;
			Time.timeScale = 0f;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;

		}
		else if (!isPaused && !MenuManager.Instance.GameMenuIsUp())
		{
			//playerCamera.LockCamera = false;
			MenuManager.Instance.CloseGameMenu();
			isPaused = false;
			Time.timeScale = 1f;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

}
