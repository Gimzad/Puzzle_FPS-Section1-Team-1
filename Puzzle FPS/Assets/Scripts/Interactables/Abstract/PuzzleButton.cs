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
    public Color InteractedColor;
    public Color OriginalColor;

    private void Awake()
    {
        OriginalColor = ButtonRenderer.sharedMaterial.color;
    }
    public virtual void Interact()
    {
        Interacted = true;
    }

    public void ChangeColor()
    {
        if (Interacted)
        {
            ButtonRenderer.sharedMaterial.color = InteractedColor;
        } else
        {
            ButtonRenderer.sharedMaterial.color = OriginalColor;
        }
    }
}
