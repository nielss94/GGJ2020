using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Station : MonoBehaviour
{
    [SerializeField]
    private bool isHoldInteraction = false;

    protected bool isActive = false;
    public bool IsActive => isActive;

    public bool GetIsHoldInteraction()
    {
        return isHoldInteraction;
    }

    public virtual void Initialize()
    {
        isActive = true;
    }

    public virtual void Terminate()
    {
        isActive = false;
    }

    public abstract void ProcessInput(InputValue value);
}
