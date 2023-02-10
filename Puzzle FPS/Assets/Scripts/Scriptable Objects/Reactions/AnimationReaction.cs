using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReaction : DelayedReaction
{
    public Animator animator;
    public string trigger;

    protected override void ImmediateReaction()
    {
        animator.SetTrigger("Activate");
    }
}
