using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCondition : EventCondition
{
    public PuzzleButton Objective;

    public override void CheckCompletion()
    {
        if (Objective.Interacted)
            satisfied = true;
        else
            satisfied = false;
    }
}
