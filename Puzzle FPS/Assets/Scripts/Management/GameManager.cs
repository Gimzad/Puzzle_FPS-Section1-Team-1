using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Components")]
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
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{ 
		BeginGame();
	}
    private void LateUpdate()
    {
        if (playStarted)
        {
			ManagePlayerTasks();
        }
    }
	#region Public Methods

	public void SetupPlayerAndCamera()
	{
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		AssertPlayerPreferencesToScript();

		playerCamera = Camera.main.GetComponent<CameraControl>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	public void InitializePlay()
	{
		MenuManager.Instance.DeactivateAllMenus();
		SceneControl.Instance.LoadFirstLevel();
		SetupPlayerAndCamera();
		HUDManager.Instance.ShowHUD();

	}
	public void RestartLevel()
	{
		//Placeholder function to restart a level without going all the way back to the main menu
		SceneControl.Instance.SceneRestart_CurrentScene();
	}
	public void AssertPlayerPreferencesToScript()
	{
		//Will take the active values from Player Preferences and assign those settings to the variables
		//used in the player and camera scripts
		//Should be called right before the player is dropped in and gains control of the player.
		//Script values should be assigned from preferences, controls should be enabled and cursor hidden
	}
	public void ToggleGameMenu()
	{
		if (MenuManager.Instance.GameMenuIsUp())
		{
			MenuManager.Instance.CloseGameMenu();
			isPaused = false;
			Time.timeScale = 1f;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;

		}
		else
		{
			MenuManager.Instance.DisplayGameMenu();
			isPaused = true;
			Time.timeScale = 0f;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;
		}
	}

	public void RestartGame()
	{
		//Call to scene control to handle unloading anything we are currently in
		SceneControl.Instance.SceneRestart_Game();
		//This call loads the main menu scene and menus
		BeginGame();

		if (!Cursor.visible)
			Cursor.visible = true;

		Cursor.lockState = CursorLockMode.Confined;
	}
    #endregion
    #region Private Methods
    void BeginGame()
	{
		isPaused = true;
		playStarted = false;

		//Deactivate any menus up from a possible last play
		DeactivateUI();

		SceneControl.Instance.LoadMainMenuScene();

		MenuManager.Instance.InitializeMenusText();

		MenuManager.Instance.DisplayMainMenu();
	}
	private void DeactivateUI()
    {
		MenuManager.Instance.DeactivateAllMenus();
		HUDManager.Instance.CloseHUD();
	}
	private void ManagePlayerTasks()
	{
		//Track by highlighting active quest or event, remove or cross out when done, add new tasks as they appear.
	}
    #endregion
}
