using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonInteract : PuzzleButton
{
    [SerializeField] Platform platform;

    Animator anim;
    public override void Interact()
    {
        base.Interact();
        anim = platform.GetComponent<Animator>();
        DragonActivation();
    }

    void DragonActivation()
    {
        anim.SetBool("Activated", true);
    }
}