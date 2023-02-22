using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : PuzzleButton
{
    public Platform platform;
    public override void Interact()
    {
        base.Interact();

        if (ObjectiveButton != null)
        {
            ObjectiveButton.ObjectiveInteraction();
        }
        ActivatePlatform();
        ChangeColor();
    }

    public void ActivatePlatform()
    {
        AnimationReaction platformReaction = ScriptableObject.CreateInstance<AnimationReaction>();
        platformReaction.instruction = 1;
        platformReaction.animator = platform.GetComponent<Animator>();
        platformReaction.text = "Active";
        platformReaction.React(platform);
    }
}
