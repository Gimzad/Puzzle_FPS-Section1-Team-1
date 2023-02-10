using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : ScriptableObject
{
    public string description;

    public EventCondition[] Conditions;

    public void EventReset()
    {
        if (Conditions == null)
            return;

        for (int i = 0; i < Conditions.Length; i++)
        {
            Conditions[i].satisfied = false;
        }
    }
    public static bool CheckCondition(EventCondition requiredCondition, EventCondition[] conditions)
    {
        EventCondition compareCondition = null;

        if (conditions != null && conditions[0] != null)
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (conditions[i] == requiredCondition)
                    compareCondition = conditions[i];
            }
        }
        //If condition was not found exit
        if (!compareCondition)
            return false;
        //else return true if the condition is satisfied
        return compareCondition.satisfied;
    }
}
