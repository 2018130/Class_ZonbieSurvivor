using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float rotateSpeed = 180f;
    [SerializeField]
    private PlayerInput input;
    private Rigidbody player_r;
    private Animator player_ani;

    private void Start()
    {
        TryGetComponent(out input);
        TryGetComponent(out player_r);
        TryGetComponent(out player_ani);
    }

    private void FixedUpdate()
    {
        Move();
        //Rotate();
        LookAt();

        player_ani.SetFloat("Move", input.MoveValue);
    }

    private void Move()
    {
        Vector3 moveDirection = input.MoveValue * transform.forward * moveSpeed * Time.deltaTime;
        player_r.MovePosition(player_r.position + moveDirection);
    }

    private void LookAt()
    {
        Vector3 mousePos = input.MousePosition;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(mousePos),out RaycastHit hit))
        {
            Vector3 target = hit.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private void Rotate()
    {
        float turn = input.RotateValue * rotateSpeed * Time.deltaTime;
        player_r.rotation = player_r.rotation * Quaternion.Euler(0, turn, 0);
    }
}
