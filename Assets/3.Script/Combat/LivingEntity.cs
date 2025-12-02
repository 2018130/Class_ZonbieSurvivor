using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
    public bool IsDead;

    [SerializeField]
    private float maxHealth;
    [SerializeField]
    protected float health;

    public event Action OnDeath;

    protected virtual void OnEnable()
    {
        health = maxHealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        IsDead = true;
        OnDeath?.Invoke();
    }
}
