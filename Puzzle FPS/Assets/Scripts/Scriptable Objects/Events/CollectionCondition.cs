using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionCondition : EventCondition
{
    //public List<CollectionItem> Objectives;
    public List<CollectionItem> Objectives;
    public List<CollectionItem> FoundObjectives;

    //Locations in-level set for spawning of objectives
    [SerializeField]
    List<Transform> ObjectiveLocations;


    private void Awake()
    {
        SpawnCollectables();
    }
    public override void CheckCompletion()
    {
        if (FoundObjectives.Count != Objectives.Count) 
        { 
            satisfied = false; 
        }
        else
        {
            for (int i = 0; i < Objectives.Count; i++)
            {
                if (!Objectives.Contains(FoundObjectives[i]))
                {

                    satisfied = false;
                }
            }
        }
        satisfied = true;
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
            Instantiate(Objectives[i], ObjectiveLocations[i]);
        }
    }
}
