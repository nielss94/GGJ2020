using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelAmountUI : MonoBehaviour
{
    private TextMeshProUGUI _fuelAmountText;
    private Furnace _furnace;

    private void Awake()
    {
        _fuelAmountText = GetComponent<TextMeshProUGUI>();
        _furnace = FindObjectOfType<Furnace>();
        _furnace.OnFurnaceFuelChanged += OnFuelChanged;   
    }

    private void Start()
    {
        OnFuelChanged(3);
    }

    private void OnFuelChanged(int fuelAmount)
    {
        _fuelAmountText.text = "" + fuelAmount;
    }
}
