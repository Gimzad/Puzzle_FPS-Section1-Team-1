using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [SerializeField] GameObject plane;
    [SerializeField] bool isDeathPlane;

    bool playerInZone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    void ActivateDeathPlane()
    {

    }
    void ActivateDamageHazard()
    {

    }
}
