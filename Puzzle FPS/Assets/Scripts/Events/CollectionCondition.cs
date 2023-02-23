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
            CollectibleItem_WeaponPickup pickup = Instantiate((Objectives[i].GetComponent<ICollectibleItem>() as CollectibleItem_WeaponPickup), ObjectiveLocations[i]);
            pickup.parentCondition = this;

        }
    }
    public void UpdateCollectionUI(CollectionCondition collection)
    {
        if (collection.ConditionUI != null)
        {
            collection.ConditionUI.ConditionToggle.isOn = satisfied;
            collection.ConditionUI.ConditionalUIText.text = "Collect all of: " + CollectionName;
            collection.ConditionUI.collectiblesText.text = FoundObjectives.Count.ToString() + "   /   " + Objectives.Count.ToString();
        }
    }

}
