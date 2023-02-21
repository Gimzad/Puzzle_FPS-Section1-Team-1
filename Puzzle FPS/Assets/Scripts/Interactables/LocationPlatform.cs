using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPlatform : InteractableArea
{
    public bool locationPathed;
    public override void InteractWithArea()
    {
        locationPathed = true;
    }
}
