using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public GameEvent[] GameEvents;

    public static GameEventManager Instance;


    public void ResetEvents()
    {
        if (GameEvents == null)
            return;
        else
        {
            for (int i = 0; i < GameEvents.Length; i++)
            {
                GameEvents[i].EventReset();
            }
        }
    }
    public static bool CheckEventConditions(EventCondition[] conditions)
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            if (conditions[i].satisfied == false)
                return false;
        }
        return true;
    }
    public static bool CheckEventCompletion(GameEvent gEvent)
    {
        GameEvent[] allEvents = Instance.GameEvents;
        bool complete = false;

        if (allEvents != null && allEvents[0] != null)
        {
            for (int i = 0; i < allEvents.Length; i++)
            {
                if (allEvents[i] == gEvent)
                    complete = CheckEventConditions(gEvent.Conditions);
            }
        }

        if (complete)
            return true;
        else
            return false;
    }
}
