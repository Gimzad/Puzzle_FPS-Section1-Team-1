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
        if (isDeathPlane && playerInZone)
        {
            ActivateDeathPlane();
        }
        //WIP 
        else if (playerInZone)
        {
            StartCoroutine(ActivateStageHazard());
        }
    }

    //detects player collided with plane
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    //Function to kill the player upon emntry of the death plane
    void ActivateDeathPlane()
    {
        GameManager.Instance.PlayerScript().TakeDamage(GameManager.Instance.PlayerScript().HP);
        playerInZone = false;
    }

    //function to be added to make a damage hazard instead of a kill plane WIP
    IEnumerator ActivateStageHazard()
    {
        do
        {
            GameManager.Instance.PlayerScript().TakeDamage(1);
            yield return new WaitForSeconds(2.5f);

        } while (playerInZone);
    }

}
