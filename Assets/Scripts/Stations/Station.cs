using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Station : MonoBehaviour
{
    public event Action<Station> OnIsRepaired = delegate(Station station) { };

    [SerializeField]
    private bool isHoldInteraction = false;
    [SerializeField]
    private bool startBroken = false;
    [SerializeField]
    private float repairTimer = 3f;

    private bool _isActive = false;
    public bool IsActive => _isActive;

    private bool _isBroken = false;
    public bool IsBroken => _isBroken;

    private float _timeLeftForRepairing;
    private bool _startedRepair;

    protected void Awake()
    {
        _timeLeftForRepairing = repairTimer;
        _isBroken = startBroken;
    }

    protected void Update()
    {
        if (_startedRepair)
        {
            _timeLeftForRepairing -= Time.deltaTime;

            DebugGUI.LogPersistent("TimeLeftRepair", $"Time left for repair: {_timeLeftForRepairing}");

            if (_timeLeftForRepairing < 0)
            {
                _startedRepair = false;
                Repair();
                OnIsRepaired.Invoke(this);
            }
        }
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

        if (_startedRepair)
            _startedRepair = false;
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

    public void StartRepair()
    {
        _startedRepair = true;
    }
}