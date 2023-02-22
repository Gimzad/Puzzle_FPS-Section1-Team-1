using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveArea : MonoBehaviour
{
    public LocationCondition parentCondition;
    public InteractableArea childArea;

    private void Awake()
    {
        childArea.Objective = this;
    }
    public void ObjectiveInteraction()
    {
        if (parentCondition != null)
            parentCondition.CheckCompletion();
    }
}
