using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Station : MonoBehaviour
{
    [SerializeField]
    private bool isHoldInteraction = false;
    [SerializeField]
    private bool startBroken = false;

    private bool _isActive = false;
    public bool IsActive => _isActive;

    private bool _isBroken = false;
    public bool IsBroken => _isBroken;

    private void Awake()
    {
        _isBroken = startBroken;
    }

    public bool GetIsHoldInteraction()
    {
        return isHoldInteraction;
    }

    public virtual void Initialize()
    {
        _isActive = true;
    }

    public virtual void Terminate()
    {
        _isActive = false;
    }

    public abstract void ProcessInput(InputValue value);
    
    public void Break()
    {
        _isBroken = true;
    }

    public void Repair()
    {
        _isBroken = false;
    }
}
