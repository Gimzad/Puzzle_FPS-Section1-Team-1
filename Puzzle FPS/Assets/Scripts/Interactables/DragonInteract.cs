using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonInteract : PuzzleButton
{
    [SerializeField] Platform platformActivate;
    [SerializeField] Platform previousPlatform;

    Animator anim;
    public override void Interact()
    {
        base.Interact();
        anim = platformActivate.GetComponent<Animator>();
        DragonActivation();
    }

    void DragonActivation()
    {
        AnimationReaction dragonReaction = ScriptableObject.CreateInstance<AnimationReaction>();
        dragonReaction.instruction = 1;
        dragonReaction.animator = anim;
        dragonReaction.text = "Activated";
        dragonReaction.React(platformActivate);
    }
}