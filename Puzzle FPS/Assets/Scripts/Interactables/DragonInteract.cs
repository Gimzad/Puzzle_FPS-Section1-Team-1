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
        anim = activePlatform.GetComponentInParent<Animator>();
        DragonActivation();
        if (toggleable || !InteractedOnce)
            ChangeColor();
        base.Interact();
        if (Objective != null)
        {
            Objective.ObjectiveInteraction();
        }
    }

    public void DragonActivation(float animSpeed = 1, bool toggle = false)
    {
        if (!toggle)
        {
            if (!toggleable)
            {
                if (!InteractedOnce)
                {
                    AnimationReaction dragonReaction = ScriptableObject.CreateInstance<AnimationReaction>();
                    dragonReaction.instruction = 1;
                    dragonReaction.animator = anim;
                    dragonReaction.text = "Activated";
                    dragonReaction.React(activePlatform);
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            anim.speed = animSpeed;
        }
    }
}