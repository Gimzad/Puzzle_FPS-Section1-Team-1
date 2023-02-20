using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlatform : InteractableArea
{
    public override void InteractWithArea()
    {
        GameManager.Instance.WinGame();
    }
}
