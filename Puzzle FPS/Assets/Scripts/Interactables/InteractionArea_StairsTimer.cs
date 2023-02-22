using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea_StairsTimer : InteractableArea
{
    [SerializeField]
    List<Stairs> staircase;

    public override void InteractWithArea()
    {
        bool wasOn = Interacted;
        base.InteractWithArea();
        if ((!wasOn && Interacted) && staircase != null) {
            Debug.Log("Interacting");
            if (staircase.Count > 0)
            {
                for (int i = 0; i < staircase.Count; i++)
                {
                    AnimationReaction activateStairs = ScriptableObject.CreateInstance<AnimationReaction>();
                    activateStairs.animator = staircase[i].GetComponent<Animator>();
                    activateStairs.instruction = 1; //boolean
                    activateStairs.delay =  i;
                    activateStairs.text = "Activated";
                    activateStairs.Init(); //needed to set delay
                    activateStairs.React(staircase[i]);
                }
            }
        }
    }
}
