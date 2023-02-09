using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("Panels")]
    public GameObject playerDamageFlashScreen;

    [Header("Images")]
    public Image playerHPBar;
    public Image reticle;

    void Awake()
    {
        Instance = this;
    }

    #region Public Methods

    public void ShowHUD()
    {
        playerHPBar.gameObject.SetActive(true);
        reticle.gameObject.SetActive(true);
    }
    public void CloseHUD()
    {
        playerHPBar.gameObject.SetActive(false);
        reticle.gameObject.SetActive(false);
    }
    #endregion
}
