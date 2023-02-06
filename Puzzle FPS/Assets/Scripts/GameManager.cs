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

	void Begin()
	{
		isPaused = true;
		playStarted = false;
		
		menuManagerInstance.DeactivateAllMenus();
		menuManagerInstance.InitializeMenusText();

		sceneControlInstance.LoadMainMenuScene();
		menuManagerInstance.DisplayMainMenu();
		
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		menuManagerInstance.DisplayMainMenu();
	}

	public void InitializeGame()
	{
		menuManagerInstance.DeactivateAllMenus();
		sceneControlInstance.LoadFirstLevel();

	}

	public void SetupPlayerAndCamera()
	{
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		menuManagerInstance.AssertMenuSettingsFromScript();

		playerCamera = Camera.main.GetComponent<CameraControl>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
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
