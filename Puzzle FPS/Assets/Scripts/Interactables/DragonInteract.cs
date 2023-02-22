using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonInteract : PuzzleButton
{
    [SerializeField] Platform activePlatform;
    [SerializeField] Platform previousPlatform;

    Animator anim;
    public override void Interact()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).length > 0)
            return;
        base.Interact();
        anim = activePlatform.GetComponent<Animator>();
        DragonActivation();
    }

    void DragonActivation()
    {
        AnimationReaction dragonReaction = ScriptableObject.CreateInstance<AnimationReaction>();
        dragonReaction.instruction = 1;
        dragonReaction.animator = anim;
        dragonReaction.text = "Activated";
        dragonReaction.React(activePlatform);
    }
}