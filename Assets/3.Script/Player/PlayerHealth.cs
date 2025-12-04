using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    private Animator ani;
    private AudioSource audioSource;
    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;

    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private AudioClip deathClip;
    [SerializeField]
    private AudioClip hitClip;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        playerMovement.enabled = true;
        playerShooter.enabled = true;
        hpBar.value = health;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);

        AudioClip damageSound = hitClip;
        if (IsDead)
        {
            damageSound = deathClip;
        }

        audioSource.PlayOneShot(damageSound);
        hpBar.value = health;
    }

    protected override void Die()
    {
        base.Die();

        playerMovement.enabled = false;
        playerShooter.enabled = false;
        hpBar.enabled = false;
        ani.SetTrigger("Die");

        UIManager.Instance.ToggleGameOverUI();
    }
}
