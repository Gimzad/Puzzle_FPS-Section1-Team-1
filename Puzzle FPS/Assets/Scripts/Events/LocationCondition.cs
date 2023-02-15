using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCondition : EventCondition
{
    public Vector3 Objective;
    public TaskListUIElement_Location ConditionUI;

    public override bool CheckCompletion()
    {
        if (GameManager.Instance.PlayerController().transform.position.ToString() == Objective.ToString())
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
        locate.ConditionUI.ConditionalUIText.text = "Get to: " + locate.Objective.ToString();
        locate.ConditionUI.LocationText.text = "Current Location: " +
            GameManager.Instance.PlayerController().transform.position.ToString();
    }
}
