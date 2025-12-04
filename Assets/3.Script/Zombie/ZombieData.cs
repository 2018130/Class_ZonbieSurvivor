using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "Scriptable/ZombieData")]
public class ZombieData : ScriptableObject
{
    public float Speed;
    public float Damage;
    public float Health;

    public Color skinColor;
}
