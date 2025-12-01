using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private Animator animator;
    private PlayerInput input;
    private Gun gun;

    float lastFireTime = 0;

    public Transform GunPivot;
    public Transform RightMount;
    public Transform LeftMount;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        gun = GetComponentInChildren<Gun>();
    }

    private void Update()
    {
        if(input.isFire)
        {
            gun.Fire();
        }
        if(input.isReload)
        {
            if(gun.Reload())
            {
                animator.SetTrigger("Reload");
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        GunPivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        animator.SetIKPosition(AvatarIKGoal.RightHand, RightMount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, RightMount.rotation);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftMount.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftMount.rotation);
    }
}
