using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[Header("UI Panels")]
    public GameObject MainMenuPanel;
	public GameObject PlayerSettingsPanel;
	public GameObject GameMenuPanel;

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
	public bool CanToggleGameMenu()
    {
		return canToggleGameMenu;
    }
	public bool GameMenuIsUp()
	{
		return gameMenuIsUp;
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
			GameMenuPanel.SetActive(false);	
	}
	public void OpenPreviousMenuPanel()
	{
		if (activeMenuPanel == PlayerSettingsPanel && !changesSaved)
        {
			//Revert back to un-changed script variables
			AssertMenuSettingsFromScript();
        }
        else
        {
			AssertMenuSettingsToScript();
        }
		//Temp object for active menu
		GameObject panelHolder;

		panelHolder = activeMenuPanel;
		activeMenuPanel.gameObject.SetActive(false);

		activeMenuPanel = previousMenuPanel;
		activeMenuPanel.gameObject.SetActive(true);

		previousMenuPanel = panelHolder;
	}

    public void InitializeMenusText()
    {
        //Will initialize all menu texts with the proper information from the scripts
    }
    #endregion
    #region Internal Menu Mehods
    private void DisplayMenuPanel(GameObject menuPanel)
	{
		if (activeMenuPanel != null)
			previousMenuPanel = activeMenuPanel;

		menuPanel.gameObject.SetActive(true);
		activeMenuPanel = menuPanel;
	}
	private void CloseActiveMenuPanel()
	{
		previousMenuPanel = activeMenuPanel;
		activeMenuPanel.gameObject.SetActive(false);
		activeMenuPanel = null;
	}
	#endregion
	#region Settings Menu Methods
	public void AssertMenuSettingsFromScript()
	{
		//Will set sliders, text, numbers, etc. to the settings in the script
	}
	public void AssertMenuSettingsToScript()
    {
		//Will take the sliders, texts, numbers, etc. and assign those settings to the variable in the script
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
