using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea_StairsTimer : InteractableArea
{
    [SerializeField]
    List<Stairs> staircase;

    public override void InteractWithArea()
    {
        if (staircase != null)
        {
            if (staircase.Count > 0)
            {
                for (int i = 0; i < staircase.Count; i++)
                {
                    AnimationReaction activateStairs = ScriptableObject.CreateInstance<AnimationReaction>();
                    activateStairs.animator = staircase[i].GetComponent<Animator>();
                    activateStairs.instruction = 1; //boolean
                    activateStairs.delay = 5 + i; //boolean
                    activateStairs.text = "Active";
                    activateStairs.React(staircase[i]);
                }
            }
        }
    }
}
