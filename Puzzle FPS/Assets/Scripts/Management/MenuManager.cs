using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	public static MenuManager Instance;

	[Header("UI Panels")]
    public GameObject MainMenuPanel;
	public GameObject PlayerSettingsPanel;
	public GameObject GameMenuPanel;
	public GameObject CreditsPanel;

    #region Internal Variables
    private GameObject previousMenuPanel;
	private GameObject activeMenuPanel;
	#endregion
	//Bool for determining if changes made in a menu will be updated to the script,
	//or if changes to the numbers, sliders, etc. should revert to their assigned values;
	private bool changesSaved;

	[Header("Menu States")]
	//Handles toggle for the in-game menu
	[SerializeField]
	private bool canToggleGameMenu;
	[SerializeField]
	private bool gameMenuIsUp;

	#region Menu Access Methods
	public bool CanToggleGameMenu
	{
		get { return canToggleGameMenu; }
		set { canToggleGameMenu = value; }
	}
	public bool GameMenuIsUp()
	{
		return gameMenuIsUp;
	}
	#endregion


	private void Awake()
    {
		Instance = this;
    }
    private void Start()
    {
		//here temorarily till menu and loading is set up
		canToggleGameMenu = true;
    }
	private void LateUpdate()
	{
		if (GameManager.Instance.PlayStarted())
			HandleInGameMenuInput();
	}
	#region Run-Time Methods
	void HandleInGameMenuInput()
	{
		//Using the menu manager here to toggle the menus, but in game manager because it affects the timescale and pausestate.
		if (canToggleGameMenu)
			if (Input.GetKeyDown(PlayerPreferences.Instance.PLAYERMENUKEY))
			{
				GameManager.Instance.ToggleGameMenu();
			}
	}
	#endregion

	#region Global Menu Methods
	public void DeactivateAllMenus()
    {
		if (MainMenuPanel.activeInHierarchy)
			MainMenuPanel.SetActive(false);
		if (PlayerSettingsPanel.activeInHierarchy)
			PlayerSettingsPanel.SetActive(false);
		if (GameMenuPanel.activeInHierarchy)
		{
			GameMenuPanel.SetActive(false);
			gameMenuIsUp = false;
		}
	}
	public void OpenPreviousMenuPanel()
	{
		if (previousMenuPanel == null)
			return;
		else
		{
			//Simple if for settings panel changes instead of separate method
			if (activeMenuPanel == PlayerSettingsPanel && !changesSaved)
			{
				//Revert back to un-changed script variables
				AssertMenuTextFromPlayerPreferences();
			}
			else
			{
				AssertMenuTextToPlayerPreferences();
			}
			//Temp object for active menu
			GameObject panelHolder;

			panelHolder = activeMenuPanel;
			activeMenuPanel.gameObject.SetActive(false);

			activeMenuPanel = previousMenuPanel;
			activeMenuPanel.gameObject.SetActive(true);

			previousMenuPanel = panelHolder;
		}
	}

    public void InitializeMenusText()
    {
		//Will initialize all menu texts with the proper information from the scripts like music volume, cmaera settings, etc.
		//Set menu to default
		AssertMenuTextFromPlayerPreferencesDefault();


		//Kind of a lazy way right now to reset the preferences to default as well as the menus.
		//Less computation can be achieved by simply calling a function in preferences that resets the variables without parsing from the menu values

		//Assign default to active preferences
		AssertMenuTextToPlayerPreferences();
	}
    #endregion

    #region Internal Menu Mehods
    private void DisplayMenuPanel(GameObject menuPanel)
	{
		if (activeMenuPanel != null)
		{
			previousMenuPanel = activeMenuPanel;
			CloseActiveMenuPanel();
		}
		activeMenuPanel = menuPanel;
		activeMenuPanel.gameObject.SetActive(true);

	}
	private void CloseActiveMenuPanel()
	{
		if (activeMenuPanel == null)
			return;
		else
		{
			previousMenuPanel = activeMenuPanel;
			activeMenuPanel.gameObject.SetActive(false);
			activeMenuPanel = null;
		}
	}
	#endregion

	#region Settings Menu Methods
	private void AssertMenuTextToPlayerPreferences()
	{
		//Will assign the values of sliders, text, numbers, etc. to the settings in the script
	}
	private void AssertMenuTextFromPlayerPreferences()
	{
		//Will set menu sliders, text, numbers, etc. from the active settings in the script
	}
	private void AssertMenuTextFromPlayerPreferencesDefault()
	{
		//Will set menu sliders, text, numbers, etc. from the default settings in the script
	}
	#endregion

	#region Specific Menu Methods
	public void DisplayMainMenu()
	{
		DisplayMenuPanel(MainMenuPanel);
		//Ensure in-game menu toggling is disabled while main menu is up.
		canToggleGameMenu = false;
	}

	public void DisplayPlayerSettingsMenu()
	{
		DisplayMenuPanel(PlayerSettingsPanel);
		//Ensure in-game menu toggling is disabled while main menu is up.
		canToggleGameMenu = false;
		//Settings menu is up, so prepare boolean for change tracking
		changesSaved = false;
	}
	public void DisplayCreditsMenu()
	{
		DisplayMenuPanel(CreditsPanel);
		//Ensure in-game menu toggling is disabled while main menu is up.
		canToggleGameMenu = false;
	}

	#endregion

	#region In-Game Menu Methods
	public void DisplayGameMenu()
	{
		//Close existing menu if up
		CloseActiveMenuPanel();
		DisplayMenuPanel(GameMenuPanel);
		//Ensure toggle of game menu is enabled
		canToggleGameMenu = true;
		//Enable bool for in-game menu
		gameMenuIsUp = true;
	}
	public void CloseGameMenu()
	{
		//Only if the game menu is active
		if (GameMenuPanel.gameObject.activeInHierarchy == true)
		{
			CloseActiveMenuPanel();
			//Ensure toggle of game menu is enabled
			canToggleGameMenu = true;
			//Disable bool for in-game menu
			gameMenuIsUp = false;
		}
	}
	#endregion
}
