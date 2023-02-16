using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class Weapon : ScriptableObject
{
    public float ShootRate;
    public int ShootDist;
    public int ShotDamage;

    public GameObject WeaponModel;
    public AudioClip WeaponAudio;
}
