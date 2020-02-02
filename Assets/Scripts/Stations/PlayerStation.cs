using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class PlayerStation : MonoBehaviour
{
    public event Action<Station> OnPossibleStationSelected = delegate(Station station) { };
    public event Action<Station> OnActiveStationSelected = delegate(Station station) { };

    public event Action<Station> OnPossibleStationDeselected = delegate(Station station) { };
    public event Action<Station> OnActiveStationDeselected = delegate(Station station) { };
    
    public event Action<Station> OnNotTheRightMaterial = delegate(Station station) {  };

    private PlayerPickUp _playerPickUp = null;
    
    private Station _possibleStation;
    private Station _activeStation;

    private bool _hasPossibleStation;
    private bool _hasActiveStation;

    private bool _canChooseStations = false;
    private bool _isRepairing = false;

    private void Awake()
    {
        _playerPickUp = GetComponentInChildren<PlayerPickUp>();
        GameManager.OnGameStarted += EnableStationChoosing;
    }

    private void EnableStationChoosing()
    {
        _canChooseStations = true;
    }

    public void OnUse()
    {
        if (_canChooseStations)
        {
            if (!_hasActiveStation)
            {
                if (!_hasPossibleStation) return;

                SetCurrentActiveStation(_possibleStation);
                RemovePossibleStation();
            }
            else
            {
                SetPossibleStation(_activeStation, true);
                RemoveCurrentActiveStation();
            }
        }
    }

    public void OnUseRelease()
    {
        if (_canChooseStations)
        {
            if (_hasActiveStation && (_activeStation.GetIsHoldInteraction() || _isRepairing))
            {
                if (_isRepairing)
                {
                    _activeStation.OnIsRepaired -= SetStationAsPossibleWhenRepaired;
                    _isRepairing = false;
                }

                SetPossibleStation(_activeStation);
                RemoveCurrentActiveStation();
            }
        }
    }

    private void SetCurrentActiveStation(Station station)
    {
        if (station.IsActive) return;

        DebugLogActiveStation(station.name);
        _hasActiveStation = true;
        _activeStation = station;
        OnActiveStationSelected.Invoke(station);

        _activeStation.Initialize();

        if (_activeStation.IsBroken)
        {
            if (_playerPickUp.CurrentPickUp != null && _playerPickUp.CurrentPickUp.GetComponent<PickUpType>().type == _activeStation.repairType)
            {
                _isRepairing = true;
                _activeStation.StartRepair();
                _activeStation.OnIsRepaired += SetStationAsPossibleWhenRepaired;
                _playerPickUp.Drop();
            }
            else
            {
                OnNotTheRightMaterial.Invoke(_activeStation);
                SetPossibleStation(_activeStation);
                RemoveCurrentActiveStation();
            }
        }
    }

    private void SetStationAsPossibleWhenRepaired(Station station)
    {
        _isRepairing = false;
        _activeStation.OnIsRepaired -= SetStationAsPossibleWhenRepaired;
        RemoveCurrentActiveStation();
        SetPossibleStation(station);
    }

    private void RemoveCurrentActiveStation()
    {
        _activeStation.Terminate();

        DebugLogActiveStation("none");
        _hasActiveStation = false;
        _activeStation = null;
        OnActiveStationDeselected.Invoke(_activeStation);
    }

    private void DebugLogActiveStation(string stationName)
    {
        DebugGUI.LogPersistent("Active station", $"Active station: {stationName}");
    }

    public void SetPossibleStation(Station station, bool ignoreActivation = false)
    {
        if (!ignoreActivation && station.IsActive) return;

        DebugLogPossibleStation(station.name);
        _hasPossibleStation = true;
        _possibleStation = station;
        OnPossibleStationSelected.Invoke(station);
    }

    public void RemovePossibleStation()
    {
        DebugLogPossibleStation("none");
        _hasPossibleStation = false;
        _possibleStation = null;
        OnPossibleStationDeselected.Invoke(_possibleStation);
    }

    public void OnMove(InputValue value)
    {
        if (_hasActiveStation)
        {
            _activeStation.ProcessInput(value);
        }
    }

    private void DebugLogPossibleStation(string stationName)
    {
        DebugGUI.LogPersistent("Possible station", $"Possible station: {stationName}");
    }
}