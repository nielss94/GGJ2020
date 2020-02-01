using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStationButtonPrompt : MonoBehaviour
{
    [SerializeField]
    private Transform buttonPromptLocation = null;
    [SerializeField]
    private GameObject useButtonPrompt = null;
    [SerializeField]
    private GameObject repairButtonPrompt = null;
    [SerializeField]
    private PlayerStation playerStation = null;

    private GameObject _useButtonPromptInstance = null;
    private GameObject _repairButtonPromptInstance = null;

    private void Awake()
    {
        _useButtonPromptInstance = Instantiate(useButtonPrompt, GameObject.FindGameObjectWithTag("UI").transform);
        _useButtonPromptInstance.SetActive(false);

        _repairButtonPromptInstance = Instantiate(repairButtonPrompt, GameObject.FindGameObjectWithTag("UI").transform);
        _repairButtonPromptInstance.SetActive(false);
    }

    private void Start()
    {
        playerStation.OnPossibleStationSelected += EnableButtonPrompt;
        playerStation.OnPossibleStationDeselected += DisableButtonPrompt;
    }

    private void OnDestroy()
    {
        playerStation.OnPossibleStationSelected -= EnableButtonPrompt;
        playerStation.OnActiveStationDeselected -= DisableButtonPrompt;
    }

    private void DisableButtonPrompt(Station station)
    {
        _useButtonPromptInstance.SetActive(false);
        _repairButtonPromptInstance.SetActive(false);
    }

    private void EnableButtonPrompt(Station station)
    {
        if (station.IsBroken)
            _repairButtonPromptInstance.SetActive(true);
        else
            _useButtonPromptInstance.SetActive(true);
    }

    private void Update()
    {
        Vector3 buttonPromptPosition = Camera.main.WorldToScreenPoint(buttonPromptLocation.position);
        
        _useButtonPromptInstance.transform.position = buttonPromptPosition;
        _repairButtonPromptInstance.transform.position = buttonPromptPosition;
    }
}