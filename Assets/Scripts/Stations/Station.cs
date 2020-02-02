using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Station : MonoBehaviour
{
    public event Action<Station> OnIsRepaired = delegate(Station station) { };
    public event Action<Station> OnIsBroken = delegate(Station station) { };

    public PickUpTypes repairType;
    public PickUpTypes fuelType;
    [SerializeField]
    private bool isHoldInteraction = false;
    [SerializeField]
    private float health = 3;
    [SerializeField]
    private bool startBroken = false;
    [SerializeField]
    private float repairTimer = 3f;

    [SerializeField]
    private ParticleSystem brokenParticles = null;
    [SerializeField]
    private ParticleSystem impactParticle = null;

    private bool _isActive = false;
    public bool IsActive => _isActive;

    private bool _isBroken = false;
    public bool IsBroken => _isBroken;

    private float _timeLeftForRepairing;

    public float TimeLeftForRepairing => _timeLeftForRepairing;

    private bool _startedRepair;
    private float _defaultHealth;

    protected void Start()
    {
        brokenParticles.Stop();
        _defaultHealth = health;
    }

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

        if (!brokenParticles.isPlaying && _isBroken)
        {
            brokenParticles.Play();
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
        OnIsBroken.Invoke(this);
        AudioManager.instance.IncreaseDanger();
    }

    public void Repair()
    {
        health = _defaultHealth;
        _isBroken = false;
        brokenParticles.Stop();
    }

    public void StartRepair()
    {
        _startedRepair = true;
    }


    public virtual void AddFuel()
    {
        // :)
    }

    public void Damage()
    {
        health--;
        Instantiate(impactParticle, transform);
        if (health <= 0)
        {
            Break();
        }
    }
}