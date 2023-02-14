using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventCondition : ScriptableObject
{
    public string description;
    protected bool satisfied;

    public bool Satisfied { get => satisfied;}

    //Updates the satisfied condition according to whatever type of event is implemented
    public virtual void CheckCompletion()
    {
    }

    public virtual void ResetCondition()
    {
        satisfied = false;
    }
}
