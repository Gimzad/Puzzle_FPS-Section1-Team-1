using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectionItem : MonoBehaviour, CollectibleItem
{
    public CollectionCondition parentCondition;
    public void Collect()
    {
        parentCondition.FoundObjectives.Add(gameObject);
        parentCondition.CheckCompletion();
    }
}
