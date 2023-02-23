using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class Weapon : ScriptableObject
{
    public Vector3 weaponPosition;
    public Vector3 weaponModelADS;
    public Vector3 weaponModelDefaultPos;

    public float ShootRate;
    public int ShootDist;
    public int ShotDamage;

    //public float zoomMax;
    //public int ZoomInSpeed;
    //public int ZoomOutSpeed;
    //public int ADSSpeed;
    //public int NotADSSpeed;


    public bool ThisIsARocketLauncher;
    public GameObject WeaponModel;
    public AudioClip WeaponAudio;
}
