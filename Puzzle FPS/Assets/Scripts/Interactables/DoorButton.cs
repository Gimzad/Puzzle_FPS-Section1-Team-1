using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : PuzzleButton
{
    [SerializeField]
    private Door LinkedDoor;
    public override void Interact()
    {
        base.Interact();
        ActivateDoor();
    }

    public void ActivateDoor()
    {
        AnimationReaction doorAction = ScriptableObject.CreateInstance<AnimationReaction>();
        doorAction.animator = LinkedDoor.GetComponent<Animator>();
        doorAction.trigger = "Activate";
        doorAction.React(LinkedDoor);
    }
}
