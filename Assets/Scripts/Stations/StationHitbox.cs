using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationHitbox : MonoBehaviour
{
    [SerializeField]
    private Station station;

    private GameManager _gameManager;

    private bool _canSetStation = false;

    private void Awake()
    {
        GameManager.OnGameStarted += EnableSettingOfStation;
    }

    private void EnableSettingOfStation()
    {
        _canSetStation = true;
    }

    public Station GetStation()
    {
        return station;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStation playerStation))
        {
            if (_canSetStation)
                playerStation.SetPossibleStation(station);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerStation playerStation))
        {
            if (_canSetStation)
                playerStation.RemovePossibleStation();
        }
    }
}