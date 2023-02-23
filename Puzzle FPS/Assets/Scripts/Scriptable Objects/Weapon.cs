using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class Weapon : ScriptableObject
{
    public float ShootRate;
    public int ShootDist;
    public int ShotDamage;

    public int ZoomInSpeed;
    public int ZoomOutSpeed;

    public int adsSpeed;
    public int NotAdsSpeed;


    public bool ThisIsARocketLauncher;
    public GameObject WeaponModel;
    public AudioClip WeaponAudio;
}
