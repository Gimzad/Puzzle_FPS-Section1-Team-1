using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate_Door : PuzzleButton, Interactable
{
    [SerializeField]
    private Door LinkedDoor;

    [SerializeField]
    private BoxCollider plateCollider;

    public override void Interact()
    {
        base.Interact();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Weighted Object"))
        {
            AnimationReaction plateAction = ScriptableObject.CreateInstance<AnimationReaction>();
            plateAction.instruction = 1;
            plateAction.animator = GetComponent<Animator>();
            plateAction.text = "Activated";
            plateAction.React(this);


            AnimationReaction doorAction = ScriptableObject.CreateInstance<AnimationReaction>();
            doorAction.instruction = 1;
            doorAction.animator = LinkedDoor.GetComponent<Animator>();
            doorAction.text = "Activated";
            doorAction.React(LinkedDoor);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Weighted Object"))
        {
            AnimationReaction plateAction = ScriptableObject.CreateInstance<AnimationReaction>();
            plateAction.instruction = 1;
            plateAction.animator = GetComponent<Animator>();
            plateAction.text = "Activated";
            plateAction.React(this);


            AnimationReaction doorAction = ScriptableObject.CreateInstance<AnimationReaction>();
            doorAction.instruction = 0;
            doorAction.animator = LinkedDoor.GetComponent<Animator>();
            doorAction.text = "Activate";
            doorAction.React(LinkedDoor);
        }
    }

}
