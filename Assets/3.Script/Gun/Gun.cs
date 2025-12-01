using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Reloading,
        Empty
    }

    private PlayerInput playerInput;
    private LineRenderer lineRenderer;
    private AudioSource audioSource;

    public ParticleSystem shotParticle;
    public ParticleSystem shellParticle;
    public Transform FireTransform;

    public GunData Data;

    private State state = State.Ready;
    private int ammoCount = 30;
    private int ammoAmount;
    private float distance = 10f;

    private float lastFireTime = 0;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerInput = GetComponentInParent<PlayerInput>();
        audioSource = GetComponent<AudioSource>();

        ammoAmount = Data.AmmoAmount;
        lastFireTime = 0;
        lineRenderer.positionCount = 2;
    }

    public void Fire()
    {
        if(ammoCount > 0 && state == State.Ready && lastFireTime + Data.shootRagTime < Time.time)
        {
            Shot();
        }
    }

    private void Shot()
    {
        Vector3 hitVector = FireTransform.position + FireTransform.forward * distance;
        ammoCount--;
        lastFireTime = Time.time;

        if (Physics.Raycast(FireTransform.position, FireTransform.forward, out RaycastHit hit, distance))
        {
            hitVector = hit.point;
            if(hit.collider.TryGetComponent(out IDamagable target))
            {
                target.OnDamage(hit.point);
            }
        }

        StartCoroutine(Shot_co(hitVector));
    }

    private IEnumerator Shot_co(Vector3 hitPoint)
    {
        lineRenderer.SetPosition(0, FireTransform.position);
        lineRenderer.SetPosition(1, hitPoint);
        lineRenderer.enabled = true;
        audioSource.PlayOneShot(Data.ShotClip);
        shotParticle.Play();
        shellParticle.Play();

        yield return new WaitForSeconds(0.03f);

        lineRenderer.enabled = false;
    }

    public bool Reload()
    {
        if(state == State.Reloading ||
            ammoCount >= Data.Magcapacity)
        {
            return false;
        }

        StartCoroutine(Reload_co());

        return true;
    }

    private IEnumerator Reload_co()
    {
        state = State.Reloading;
        int fillAmount = Data.Magcapacity - ammoCount;
        if(ammoAmount < fillAmount)
        {
            fillAmount = ammoAmount;
        }
        ammoAmount -= fillAmount;
        ammoCount += fillAmount;
        audioSource.PlayOneShot(Data.ReloadClip);

        yield return new WaitForSeconds(Data.reloadTime);

        state = State.Ready;
    }
}
