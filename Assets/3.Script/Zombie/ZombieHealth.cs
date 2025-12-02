using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : LivingEntity
{
    private NavMeshAgent navMeshAgent;

    [Header("Zombie")]
    [SerializeField]
    private LayerMask targetLayerMask;
    private LivingEntity target;

    [SerializeField]
    private float lastAttackTime = 0.5f;
    [SerializeField]
    private float zombieAttackbet;
    [SerializeField]
    private float damage = 10f;

    private Animator ani;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip hitClip;
    [SerializeField]
    private AudioClip deathClip;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(FindTarget_co());
    }

    private IEnumerator FindTarget_co()
    {
        while(!IsDead)
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, 20f, targetLayerMask);
            bool isFindTarget = false;

            foreach(var c in colls)
            {
                if(c.TryGetComponent(out LivingEntity p))
                {
                    target = p;
                    ani.SetBool("HasTarget", true);
                    isFindTarget = true;
                    break;
                }
            }

            if(!isFindTarget)
            {
                ani.SetBool("HasTarget", false);
            }

            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(!target.IsDead && Time.time >= lastAttackTime + zombieAttackbet)
        {
            if(other.TryGetComponent(out LivingEntity e) && e.Equals(target))
            {
                lastAttackTime = Time.time;
                Vector3 closestPos = other.ClosestPoint(transform.position);
                Vector3 normal = transform.position - other.transform.position;

                e.OnDamage(damage, closestPos, normal);
            }
        }
    }

    private void Update()
    {
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        if(target != null && !target.IsDead && !IsDead)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.transform.position);

            transform.LookAt(target.transform);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);

        AudioClip damageSound = hitClip;

        audioSource.PlayOneShot(damageSound);
    }

    protected override void Die()
    {
        base.Die();

        Collider[] colls = GetComponents<Collider>();
        foreach(var c in colls)
        {
            c.enabled = false;
        }

        navMeshAgent.isStopped = true;
        audioSource.PlayOneShot(deathClip);
        ani.SetTrigger("Die");
    }
}
