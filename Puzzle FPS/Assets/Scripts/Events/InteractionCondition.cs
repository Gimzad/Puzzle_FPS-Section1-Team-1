using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCondition : EventCondition
{
    public PuzzleButton Objective;

    public override bool CheckCompletion()
    {
        if (Objective.Interacted)
            satisfied = true;
        else
            satisfied = false;

        return base.CheckCompletion();
    }
}
