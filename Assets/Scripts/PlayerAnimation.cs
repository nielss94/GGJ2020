﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator = null;
    private Player _player = null;
    private PlayerStation _playerStation = null;

    private Station _curStation = null;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _player = GetComponent<Player>();
        _playerStation = GetComponent<PlayerStation>();
    }

    private void Start()
    {
        _playerStation.OnActiveStationSelected += SetActiveStationSelected;
        _playerStation.OnActiveStationDeselected += SetActiveStationDeselected;
    }

    private void Update()
    {
        //CHECK IF PLAYER IS IN STATION AND PLAY ANIMATION
        if (_curStation == null)
        {
            _animator.SetBool("Walking", _player.MoveInput != Vector3.zero);
        }
    }

    private void SetActiveStationSelected(Station station)
    {
        _curStation = station;
        _animator.SetBool("Walking", false);

        switch (station)
        {
            case SteeringWheel _:
                _animator.SetBool("IdleAtWheel", true);
                break;
            case Railing _:
                break;
        }
    }
    
    private void SetActiveStationDeselected(Station station)
    {
        switch (_curStation)
        {
            case SteeringWheel _:
                _animator.SetBool("IdleAtWheel", false);
                break;
            case Railing _:
                break;
        }

        _curStation = null;
    }
}