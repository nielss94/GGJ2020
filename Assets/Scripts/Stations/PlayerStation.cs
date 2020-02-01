using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerStation : MonoBehaviour
{
    public event Action<Station> OnPossibleStationSelected = delegate(Station station) { };
    public event Action<Station> OnActiveStationSelected = delegate(Station station) { };

    public event Action<Station> OnPossibleStationDeselected = delegate(Station station) { };
    public event Action<Station> OnActiveStationDeselected = delegate(Station station) { };

    private Station _possibleStation;
    private Station _activeStation;

    private bool _hasPossibleStation;
    private bool _hasActiveStation;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!_hasActiveStation)
            {
                SetCurrentActiveStation(_possibleStation);
                RemovePossibleStation();
            }
            else
            {
                SetPossibleStation(_activeStation);
                RemoveCurrentActiveStation();
            }
        }

        if (_hasActiveStation && _activeStation.GetIsHoldInteraction())
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                SetPossibleStation(_activeStation);
                RemoveCurrentActiveStation();
            }
        }
        
        if(_hasActiveStation)
            _activeStation.Execute();
    }

    private void SetCurrentActiveStation(Station station)
    {
        DebugLogActiveStation(station.name);
        _hasActiveStation = true;
        _activeStation = station;
        OnActiveStationSelected.Invoke(station);
    }

    private void RemoveCurrentActiveStation()
    {
        DebugLogActiveStation("none");
        _hasActiveStation = false;
        _activeStation = null;
        OnActiveStationDeselected.Invoke(_activeStation);
    }

    private void DebugLogActiveStation(string stationName)
    {
        DebugGUI.LogPersistent("Active station", $"Active station: {stationName}");
    }

    public void SetPossibleStation(Station station)
    {
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

    private void DebugLogPossibleStation(string stationName)
    {
        DebugGUI.LogPersistent("Possible station", $"Possible station: {stationName}");
    }
}