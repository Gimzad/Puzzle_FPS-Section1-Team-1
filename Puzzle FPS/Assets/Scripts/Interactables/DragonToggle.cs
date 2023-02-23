using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonToggle : DragonInteract
{
    [SerializeField] DragonInteract statue;
    [SerializeField] GameObject ChildDragon;
    [SerializeField] GameObject ChildStatue;

    bool turnedOn;
    bool platformStopped;

    private void Awake()
    {
        gameObject.layer = 0;
    }

    void Update()
    {
        if(statue.InteractedOnce && turnedOn == false)
        {
            SetMaterial();
            turnedOn = true;
            gameObject.layer = 10;
        }
    }
    void SetMaterial()
    {
        ChildDragon.GetComponent<Renderer>().material = InteractedMaterial;
        ChildStatue.GetComponent<Renderer>().material = InteractedMaterial;
    }

    public override void Interact()
    {
        if(turnedOn)
        {
            float speed = platformStopped ? 1 : 0;
            Debug.Log(speed);
            statue.DragonActivation(speed);
        }
        platformStopped = !platformStopped;
    }
}
