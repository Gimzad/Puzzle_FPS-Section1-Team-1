using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleButton : MonoBehaviour, Interactable
{
    [HideInInspector]
    public ObjectiveButton ObjectiveButton;

    public bool Interacted;
    public virtual void Interact()
    {
        Interacted = true;
    }
}
