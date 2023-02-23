using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class RocketLauncher : Weapon
{
    [SerializeField] GameObject explosion;
    public int explosionDamage;

    private void Awake()
    {
        Explosion explode = explosion.GetComponent<Explosion>();
        explode.explosionDamage = explosionDamage;
    }
}
