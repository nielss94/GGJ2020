using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator = null;
    private Player _player = null;
    private PlayerStation _playerStation = null;
    private PlayerPickUp _playerPickUp = null;
    private Station _curStation = null;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _player = GetComponent<Player>();
        _playerPickUp = GetComponentInChildren<PlayerPickUp>();
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
        
        _animator.SetBool("HoldingItem", _playerPickUp.HasCurrentPickup);
    }

    private void SetActiveStationSelected(Station station)
    {
        _curStation = station;
        _animator.SetBool("Walking", false);

        if (station.IsBroken)
        {
            if(!_animator.GetBool("Repairing"))
                _animator.SetBool("Repairing", true);
            return;
        }
        
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
        _animator.SetBool("Repairing", false);
        
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
