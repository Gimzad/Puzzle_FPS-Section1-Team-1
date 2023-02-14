using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
	[Header("Core Objects")]
	[SerializeField]
	GameObject PlayerPrefab;

	GameObject playerObject;
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
	public CameraControl CameraControl()
	{
		return playerCamera;
	}
	public bool IsPaused()
    {
        return isPaused;
    }
	public bool PlayStarted()
	{
		return playStarted;
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
		//BeginGame();
		InitializePlay();
	}
    private void LateUpdate()
    {
        if (playStarted)
        {
			ManagePlayerTasks();
        }
		
    }
    #region Public Methods

    #region Game Goal Methods
	public void UpdateEnemyCount(int amt)
    {
		enemiesRemaining += amt;
    }
    #endregion
    public void InitializePlay()
	{
		//MenuManager.Instance.DeactivateAllMenus();
		//SceneControl.Instance.LoadFirstLevel();

		SetupPlayerAndCamera();
		HUDManager.Instance.ShowHUD();
		playStarted = true;
	}
	public void SetupPlayerAndCamera()
	{
		playerObject = Instantiate(PlayerPrefab);
		playerController = playerObject.GetComponent<PlayerController>();
		playerCamera = Camera.main.GetComponent<CameraControl>();

		AssertPlayerPreferencesToScript();

		Cursor.lockState = CursorLockMode.Locked;

		playerCamera.ToggleCursorVisibility();
		MenuManager.Instance.CanToggleGameMenu = true;

		GameEventManager.Instance.GenerateEvents();
	}
    public void AssertPlayerPreferencesToScript()
	{
        //Will take the active values from Player Preferences and assign those settings to the variables
        //used in the player and camera scripts
        //Should be called right before the player is dropped in and gains control of the player.
        //Script values should be assigned from preferences, controls should be enabled and cursor hidden
        playerController.HP = PlayerPreferences.Instance.HP;
		playerController.MoveSpeed = PlayerPreferences.Instance.MoveSpeed;
		playerController.JumpTimes = PlayerPreferences.Instance.JumpMax;
		playerController.JumpSpeed = PlayerPreferences.Instance.JumpSpeed;
		playerController.PlayerGravity = PlayerPreferences.Instance.PlayerGravityStrength;


		playerController.ShootRate = PlayerPreferences.Instance.ShootRate;
		playerController.ShootDistance = PlayerPreferences.Instance.ShootDistance;
		playerController.ShotDamage = PlayerPreferences.Instance.ShotDamage;

		playerCamera.HorizontalSensitivity = PlayerPreferences.Instance.SensitivityHorizontal;
		playerCamera.VeritcalSensitivity = PlayerPreferences.Instance.SensitivityVertical;
		playerCamera.VeticalLockMin = PlayerPreferences.Instance.VerticalLockMin;
		playerCamera.VeticalLockMax = PlayerPreferences.Instance.VerticalLockMax;
		playerCamera.InvertX = PlayerPreferences.Instance.InvertX;

	}

	public void RestartLevel()
	{
		Destroy(playerObject);
		Debug.Log("Restarting Level");
		//Restart a level without going all the way back to the main menu
		SceneControl.Instance.SceneRestart_CurrentScene();

		//reload player and variable settings
		InitializePlay();
	}
	public void RestartGame()
	{
		playerCamera.ToggleCursorVisibility();
		Destroy(playerObject);

		//Call to scene control to handle unloading anything we are currently in
		SceneControl.Instance.SceneRestart_Game();

		//This call loads the main menu scene and menus
		BeginGame();

		Cursor.lockState = CursorLockMode.Confined;
	}
	public void LoseGame()
	{
		Pause();

		MenuManager.Instance.DisplayLoseMenu();
	}
	public void WinGame()
	{
		Pause();

		MenuManager.Instance.DisplayWinMenu();
	}
	public void ToggleGameMenu()
	{
		if (MenuManager.Instance.GameMenuIsUp())
		{
			MenuManager.Instance.CloseGameMenu();
			UnPause();
			playerCamera.ToggleCursorVisibility();

		}
		else
		{
			MenuManager.Instance.DisplayGameMenu();
			Pause();
			playerCamera.ToggleCursorVisibility();
		}
	}
    #endregion
    #region Private Methods
    private void Pause()
    {
		isPaused = true;
		Time.timeScale = 0f;
		Cursor.lockState = CursorLockMode.Confined;
	}
	private void UnPause()
	{
		isPaused = false;
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
	}
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
		GameEventManager.Instance.UpdateEvents();
	}
    #endregion
}
