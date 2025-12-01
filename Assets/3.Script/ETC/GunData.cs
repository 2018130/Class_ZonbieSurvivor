using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable/GunData")]
public class GunData : ScriptableObject
{
    public AudioClip ShotClip;
    public AudioClip ReloadClip;

    public int AmmoAmount;
    public int Magcapacity;

    public float reloadTime;
    public float shootRagTime;
}
