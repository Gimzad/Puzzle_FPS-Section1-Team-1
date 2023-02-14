using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : ScriptableObject
{
    //Events in the game that presetn description in UI
    public string description;
    //Each event has a list of EventConditions like Collection, Location, Interaction
    public List<EventCondition> Conditions;

    public void EventReset()
    {
        //Reset event and all conditions to initial states
        if (Conditions == null)
            return;

        for (int i = 0; i < Conditions.Count; i++)
        {
            Conditions[i].ResetCondition();
        }
    }
    public bool CheckConditionState(EventCondition searchCondition, List<EventCondition> conditions, out bool notFound)
    {
        EventCondition storedCondition;
        if (!conditions.Contains(searchCondition))
        {
            notFound = true;
            return false;
        }
        else
        {
            notFound = false;
            int location = conditions.IndexOf(searchCondition);
            storedCondition = conditions[location];
        }

        //If condition was not found exit
        if (!storedCondition.Satisfied)
            return false;
        else
            //else return true if the condition is satisfied
            return true;
    }
    public bool CheckEventConditions(List<EventCondition> conditions)
    {
        //Check if an Event's conditions are completed
        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i].Satisfied == false)
                return false;
        }
        return true;
    }
}
