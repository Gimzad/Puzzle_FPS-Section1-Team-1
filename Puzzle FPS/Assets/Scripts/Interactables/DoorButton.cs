using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : PuzzleButton
{
    [SerializeField]
    private Door LinkedDoor;

    private Animator doorAnimator;

    public bool onDoor;
    private void Awake()
    {
        doorAnimator = LinkedDoor.GetComponent<Animator>();
    }
    public override void Interact()
    {
        if (doorAnimator.IsInTransition(0))
            return;
        Debug.Log("INTERACTED");
        base.Interact();
        ActivateDoor();
        if (!onDoor)
            ChangeColor();
    }

    public void ActivateDoor()
    {
        AnimationReaction doorAction = ScriptableObject.CreateInstance<AnimationReaction>();
        doorAction.instruction = 1;
        doorAction.animator = doorAnimator;
        doorAction.text = "Activated";
        doorAction.React(LinkedDoor);
    }
}
