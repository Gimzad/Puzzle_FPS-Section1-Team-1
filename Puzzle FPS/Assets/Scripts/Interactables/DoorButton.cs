using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : PuzzleButton
{
    [SerializeField]
    private Door LinkedDoor;
    public override void Interact()
    {
        ActivateDoor();
    }

    public void ActivateDoor()
    {
        AnimationReaction doorAction = new AnimationReaction();
        doorAction.animator = LinkedDoor.GetComponent<Animator>();
        doorAction.trigger = "Activate";
    }
}
