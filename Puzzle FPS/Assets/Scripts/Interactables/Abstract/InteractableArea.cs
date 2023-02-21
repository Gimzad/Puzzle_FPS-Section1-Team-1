using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class InteractableArea : MonoBehaviour
{
    [SerializeField] 
    MeshRenderer areaRenderer;

    public bool Interacted;
    public Color InteractedColor;
    public Color OriginalColor;

    private void Awake()
    {
        areaRenderer = GetComponent<MeshRenderer>();
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
            areaRenderer.sharedMaterial.color = InteractedColor;
        }
        else
        {
            areaRenderer.sharedMaterial.color = OriginalColor;
        }
    }
}
