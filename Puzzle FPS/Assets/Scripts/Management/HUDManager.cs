using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("Panels")]
    public GameObject PlayerDamageFlashScreen;
    public GameObject TaskList;
    public GameObject InputReaderText;

    [Header("Images")]
    [SerializeField]
    private Image playerHPBar;
    [SerializeField]
    private Image playerHPFill;

    public Image reticle;

    void Awake()
    {
        Instance = this;
    }
    #region Access Methods

    public Image PlayerHPBar
    {
        get { return playerHPBar; }
        set { playerHPBar = value; }
    }
    #endregion
    #region Public Methods
    public void UpdateHPBarFill(float amt)
    {
        playerHPFill.fillAmount = amt;
    }
    public void ShowHUD()
    {
        playerHPBar.gameObject.SetActive(true);
        reticle.gameObject.SetActive(true);
        TaskList.gameObject.SetActive(true);
        InputReaderText.gameObject.SetActive(true);
    }
    public void CloseHUD()
    {
        playerHPBar.gameObject.SetActive(false);
        reticle.gameObject.SetActive(false);
        TaskList.gameObject.SetActive(false);
        InputReaderText.gameObject.SetActive(false);
    }
    #endregion
}
