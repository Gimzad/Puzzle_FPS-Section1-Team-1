using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveButton : PuzzleButton
{
    public InteractCondition parentCondition;
    public override void Interact()
    {
        ObjectiveInteraction();
    }

    public void ObjectiveInteraction()
    {
        Interacted = true;
        parentCondition.CheckCompletion();
    }
}
