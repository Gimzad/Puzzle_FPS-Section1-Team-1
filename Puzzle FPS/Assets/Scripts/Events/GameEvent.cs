using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public GameObject playerSpawnPos;
    //Title of event for UI description
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

    public void UpdateConditionsCompletion(List<EventCondition> conditions)
    {
        //Check if an Event's conditions are completed
        for (int i = 0; i < conditions.Count; i++)
        {
            conditions[i].CheckCompletion();
        }
    }
    public bool ReturnEventCompletion(List<EventCondition> conditions)
    {
        //Check if an Event's conditions are completed
        for (int i = 0; i < conditions.Count; i++)
        {
            if (!conditions[i].Satisfied)
            {
                return false;
            }
        }
        return true;
    }
}
