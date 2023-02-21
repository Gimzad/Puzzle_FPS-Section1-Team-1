using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem_WeaponPickup : CollectionItem
{
    public WeaponPickup attachedPickup;

    private void Awake()
    {
        attachedPickup.parentCollectible = this;
        Instantiate(attachedPickup, transform);
    }
}
