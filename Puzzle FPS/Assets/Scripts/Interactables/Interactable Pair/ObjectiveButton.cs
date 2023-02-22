using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveButton : MonoBehaviour
{
    public InteractionCondition parentCondition;
    public PuzzleButton childButton;

    private void Awake()
    {
        childButton.Objective = this;
    }
    public void ObjectiveInteraction()
    {
        if (parentCondition != null)
            parentCondition.CheckCompletion();
    }
}
