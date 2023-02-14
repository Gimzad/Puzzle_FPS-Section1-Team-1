using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCondition : EventCondition
{
    public Vector3 Objective;

    public override void CheckCompletion()
    {
        if (GameManager.Instance.PlayerController().transform.position == Objective)
        {
            satisfied = true;
        }
        else
        {
            satisfied = false;
        }
    }
}
