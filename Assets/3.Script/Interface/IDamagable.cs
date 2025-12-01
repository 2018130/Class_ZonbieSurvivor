using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void OnDamage(Vector3 hitPoint);
}
