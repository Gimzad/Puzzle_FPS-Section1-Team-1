using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForAnimationToFinish : CustomYieldInstruction
{

    private readonly string animationName;

    private readonly Animator animator;
    private readonly int layerIndex;


    private AnimatorStateInfo StateInfo => animator.GetCurrentAnimatorStateInfo(layerIndex);

    private bool CorrectAnimationIsPlaying => StateInfo.IsName(animationName);

    private bool AnimationIsDone => StateInfo.normalizedTime == 1;
    
    public override bool keepWaiting => CorrectAnimationIsPlaying && !AnimationIsDone;

    public WaitForAnimationToFinish(Animator animator, string animationName, int layerIndex = 0)
    {

        this.animator = animator;
        this.layerIndex = layerIndex;
        this.animationName = animationName;
        
    }

}
