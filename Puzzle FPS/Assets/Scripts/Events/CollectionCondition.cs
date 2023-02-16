using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionCondition : EventCondition
{
    public string CollectionName;

    //public List<CollectionItem> Objectives;
    public List<GameObject> Objectives;
    public List<GameObject> FoundObjectives;

    //Locations in-level set for spawning of objectives
    [SerializeField]
    List<Transform> ObjectiveLocations;

    public TaskListUI_Collection ConditionUI;

    private void Awake()
    {
        SpawnCollectables();
    }
    public override bool CheckCompletion()
    {
        if (FoundObjectives.Count != Objectives.Count) 
        { 
            satisfied = false;
            return base.CheckCompletion();
        }
        else
        {
            for (int i = 0; i < Objectives.Count; i++)
            {
                if (!Objectives.Contains(FoundObjectives[i].GetComponent<GameObject>()))
                {

                    satisfied = false;
                    return base.CheckCompletion();
                }
            }
        }
        satisfied = true;
        return base.CheckCompletion();
    }
    public override void ResetCondition()
    {
        base.ResetCondition();
        FoundObjectives.Clear();
    }

    public void SpawnCollectables()
    {
        for (int i = 0; i < Objectives.Count; i++)
        {
            Instantiate((Objectives[i].GetComponent<CollectibleItem>() as CollectibleItem_WeaponPickup).attachedPickup, ObjectiveLocations[i]);
            (Objectives[i].GetComponent<CollectibleItem>() as CollectibleItem_WeaponPickup).parentCondition = this;

        }
    }
    public void UpdateCollectionUI(CollectionCondition collection)
    {
        collection.ConditionUI.ConditionToggle.isOn = satisfied;
        collection.ConditionUI.ConditionalUIText.text = "Collect all of: " + CollectionName;
        collection.ConditionUI.collectiblesText.text = FoundObjectives.Count.ToString() + "   /   " + Objectives.Count.ToString();
    }

}
