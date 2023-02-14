using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    //A class with a static instance like GameManager to handle the GameEvents that are created and the conditions for each
    public List<GameEvent> GameEvents;

    public static GameEventManager Instance;


    public void ResetEvents()
    {
        //If no events active just back out
        if (GameEvents == null)
            return;
        else
        {
            //otherwise call the EventReset
            for (int i = 0; i < GameEvents.Count; i++)
            {
                GameEvents[i].EventReset();
            }
        }
    }
    public static bool CheckEventCompletion(GameEvent gEvent)
    {
        //Check if an Event's conditions are completed
        return gEvent.CheckEventConditions(gEvent.Conditions);
    }
    public static bool EventListComplete()
    {
        List<GameEvent> allEvents = Instance.GameEvents;

        if (allEvents != null && allEvents[0] != null)
        {
            for (int i = 0; i < allEvents.Count; i++)
            {
                if (!CheckEventCompletion(allEvents[i]))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
