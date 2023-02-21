using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weapon;

    [HideInInspector]
    public CollectibleItem_WeaponPickup parentCollectible;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.PlayerScript().PickupWeapon(weapon);

            if (parentCollectible != null)
            {
                parentCollectible.Collect();
            }
            Destroy(gameObject);
        }
    }
}
