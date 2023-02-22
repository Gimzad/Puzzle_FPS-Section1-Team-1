using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleButton : MonoBehaviour, Interactable
{
    [SerializeField]
    MeshRenderer ButtonRenderer;

    [HideInInspector]
    public ObjectiveButton ObjectiveButton;

    public bool Interacted;
    public bool PermanentlyOn;
    public Material InteractedMaterial;
    public Material OriginalMaterial;

    private void Awake()
    {
        OriginalMaterial = ButtonRenderer.sharedMaterial;
    }
    public virtual void Interact()
    {
        if (PermanentlyOn)
            Interacted = true;
        else
            Interacted = !Interacted;
    }

    public void ChangeColor()
    {
        if (Interacted)
        {
            ButtonRenderer.sharedMaterial = InteractedMaterial;
        } else
        {
            ButtonRenderer.sharedMaterial = OriginalMaterial;
        }
    }
}
