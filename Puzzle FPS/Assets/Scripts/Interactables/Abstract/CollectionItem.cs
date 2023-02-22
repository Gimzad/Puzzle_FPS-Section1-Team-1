using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectionItem : MonoBehaviour, ICollectibleItem
{
    public CollectionCondition parentCondition;
    public void Collect()
    {
        parentCondition.FoundObjectives.Add(gameObject);
        parentCondition.CheckCompletion();
    }
}
