using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Furnace : Station
{
    public event Action<int> OnFurnaceFuelChanged = delegate(int fuelLevel) { };
    public float fuelDescreaseTimer = 5f;

    public int _currentFuelLevel = 3;
    private bool gameStarted = false;

    public void OnEnable()
    {
        OnIsRepaired += OnRepair;
        GameManager.OnGameStarted += OnGameStart;
    }

    public override void AddFuel()
    {
        _currentFuelLevel++;
    }

    public override void ProcessInput(InputValue value)
    {

    }

    public void OnDisable()
    {
        OnIsRepaired -= OnRepair;
    }

    private void OnRepair(Station station)
    {
        StartCoroutine(FuelDecreaseRoutine());
    }
    
    private void OnGameStart()
    {
        gameStarted = true;
        StartCoroutine(FuelDecreaseRoutine());
    }

    private IEnumerator FuelDecreaseRoutine()
    {
        while (!IsBroken && gameStarted)
        {
            yield return new WaitForSeconds(fuelDescreaseTimer);
            if (_currentFuelLevel > 0) 
            {
                _currentFuelLevel--;
                OnFurnaceFuelChanged.Invoke(_currentFuelLevel);
            }
        }
    }
}
