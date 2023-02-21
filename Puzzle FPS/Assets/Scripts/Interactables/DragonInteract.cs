using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonInteract : PuzzleButton
{
    [SerializeField] GameObject activated_object;
    public override void Interact()
    {
        base.Interact();
        DragonActivation();
    }

    void DragonActivation()
    {

    }
}