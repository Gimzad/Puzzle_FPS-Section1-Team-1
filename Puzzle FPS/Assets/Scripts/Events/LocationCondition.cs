using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCondition : EventCondition
{
    public LocationPlatform Objective;
    public TaskListUI_Location ConditionUI;

    public override bool CheckCompletion()
    {
        if (Objective.locationPathed)
        {
            satisfied = true;
        }
        else
        {
            satisfied = false;
        }
        return base.CheckCompletion();
    }
    public void UpdateLocationUI(LocationCondition locate)
    {
        locate.ConditionUI.ConditionToggle.isOn = satisfied;
        locate.ConditionUI.ConditionalUIText.text = "Get to: " + locate.Objective.name;
        locate.ConditionUI.LocationText.text = "Current Location: " +
            GameManager.Instance.PlayerScript().transform.position.ToString();
    }
}
