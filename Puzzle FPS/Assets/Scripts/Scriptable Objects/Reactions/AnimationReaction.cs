using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReaction : DelayedReaction
{
    public Animator animator;
    public string trigger;

    private int triggerHash;

    protected override void SpecificInit()
    {
        triggerHash = Animator.StringToHash(trigger);
    }

    protected override void ImmediateReaction()
    {
        if (trigger != null)
            animator.SetTrigger(triggerHash);
    }
}
