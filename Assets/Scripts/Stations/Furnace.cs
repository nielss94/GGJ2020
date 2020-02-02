using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Furnace : Station
{
    public PickUpTypes fuelType = PickUpTypes.Wood;
    public float fuelDescreaseTimer = 5f;

    private int _currentFuelLevel = 3;

    public void OnEnable()
    {
        OnIsRepaired += OnRepair;
    }

    public void Initialize()
    {
        base.Initialize();

        StartCoroutine(FuelDecreaseRoutine());
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

    private IEnumerator FuelDecreaseRoutine()
    {
        while (!IsBroken)
        {
            yield return new WaitForSeconds(fuelDescreaseTimer);
            if (_currentFuelLevel > 0) _currentFuelLevel--;
        }
    }
}
