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
        anim = activePlatform.GetComponent<Animator>();
        DragonActivation();
        base.Interact();
    }

    public void DragonActivation(float animSpeed = 1, bool toggle = false)
    {

        if (!toggle)
        {
            if (!InteractedOnce)
            {
                AnimationReaction dragonReaction = ScriptableObject.CreateInstance<AnimationReaction>();
                dragonReaction.instruction = 1;
                dragonReaction.animator = anim;
                dragonReaction.text = "Activated";
                Debug.Log("Waiting");
                PlatformStart();
                Debug.Log("Waiting Done");
                dragonReaction.React(activePlatform);
            }
            else
            {
                return;
            }
        } else
        {
            anim.speed = animSpeed;
        }
    }
    IEnumerator PlatformStart()
    {
        yield return new WaitForAnimationToStart(anim, "BridgePattern_1", 0);
    }
    IEnumerator PlatformFinish()
    {

        yield return new WaitForAnimationToFinish(anim, "BridgePattern_1", 0);
    }
}