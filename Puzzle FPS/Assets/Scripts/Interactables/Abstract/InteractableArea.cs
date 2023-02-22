using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class InteractableArea : MonoBehaviour
{
    [SerializeField] 
    MeshRenderer areaRenderer;

    public bool Interacted;
    public Material InteractedMaterial;
    public Material OriginalMaterial;

    private void Awake()
    {
        if (areaRenderer != null)
            OriginalMaterial = areaRenderer.sharedMaterial;
    }
    public virtual void InteractWithArea()
    {
        ChangeColor();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interacted = true;
            InteractWithArea();
        }
    }
    public void ChangeColor()
    {
        if (Interacted)
        {
            areaRenderer.sharedMaterial = InteractedMaterial;
        }
        else
        {
            areaRenderer.sharedMaterial = OriginalMaterial;
        }
    }
}
