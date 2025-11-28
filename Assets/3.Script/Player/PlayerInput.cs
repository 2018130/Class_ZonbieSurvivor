using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float MoveValue { get; set; }
    public float RotateValue { get; set; }
    public Vector2 MousePosition { get; set; }

    public bool isFire { get; set; }
    public bool isReload { get; set; }

    [SerializeField]
    private float forwardValue = 0f;
    [SerializeField]
    private Vector2 mouseValue = Vector2.zero;

    public void Event_Move(InputAction.CallbackContext context)
    {
        if(context.phase.Equals(InputActionPhase.Performed))
        {
            forwardValue = (float)context.ReadValue<float>();
        }
        else if(context.phase.Equals(InputActionPhase.Canceled))
        {
            forwardValue = 0;
        }

        MoveValue = forwardValue;
    }
    public void Event_Rotation(InputAction.CallbackContext context)
    {
        if (context.phase.Equals(InputActionPhase.Performed))
        {
            mouseValue = context.ReadValue<Vector2>();
        }
        else if (context.phase.Equals(InputActionPhase.Canceled))
        {
            //mouseValue = Vector2.zero;
        }

        MousePosition = mouseValue;
    }

    public void Event_Fire(InputAction.CallbackContext context)
    {
        if(context.phase.Equals(InputActionPhase.Performed))
        {
            isFire = true;
        }
        else if(context.phase.Equals(InputActionPhase.Canceled))
        {
            isFire = false;
        }
    }
    public void Event_Reload(InputAction.CallbackContext context)
    {
        if (context.phase.Equals(InputActionPhase.Performed))
        {
            isReload = true;
        }
        else if (context.phase.Equals(InputActionPhase.Canceled))
        {
            isReload = false;
        }
    }

}
