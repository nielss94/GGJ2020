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
    private GameObject buttonPrompt = null;
    [SerializeField]
    private PlayerStation playerStation = null;
    
    private GameObject _buttonPromptInstance = null;
    
    private void Awake()
    {
        _buttonPromptInstance = Instantiate(buttonPrompt, GameObject.FindGameObjectWithTag("UI").transform);
        _buttonPromptInstance.SetActive(false);
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

    private void DisableButtonPrompt(Station obj)
    {
        _buttonPromptInstance.SetActive(false);
    }

    private void EnableButtonPrompt(Station obj)
    {
        _buttonPromptInstance.SetActive(true);
    }

    private void Update()
    {
        Vector3 buttonPromptPosition = Camera.main.WorldToScreenPoint(buttonPromptLocation.position);
        _buttonPromptInstance.transform.position = buttonPromptPosition;
    }
}
