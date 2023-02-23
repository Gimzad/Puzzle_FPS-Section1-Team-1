using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : PuzzleButton
{
    public Platform platform;
    public override void Interact()
    {
        base.Interact();

        if (Objective != null)
        {
            Objective.ObjectiveInteraction();
        }
        ActivatePlatform();
        ChangeColor();
    }

    public void ActivatePlatform()
    {
        AnimationReaction platformReaction = ScriptableObject.CreateInstance<AnimationReaction>();
        platformReaction.instruction = 1;
        platformReaction.animator = platform.GetComponentInParent<Animator>();
        platformReaction.text = "Active";
        platformReaction.React(platform);
    }
}
