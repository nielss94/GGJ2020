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
    private GameObject repairingImagePrompt = null;
    [SerializeField]
    private PlayerStation playerStation = null;
    [SerializeField]
    private Transform materialPromptLocation = null;
    [SerializeField]
    private Transform repairPromptLocation = null;
    [SerializeField]
    private GameObject needMaterialsPrompt = null;
    

    private GameObject _useButtonPromptInstance = null;
    private GameObject _repairButtonPromptInstance = null;
    private GameObject _repairingImagePromptInstance = null;
    private GameObject _materialPromptInstance = null;
    

    private void Awake()
    {
        _useButtonPromptInstance = Instantiate(useButtonPrompt, GameObject.FindGameObjectWithTag("UI").transform);
        _useButtonPromptInstance.SetActive(false);

        _repairButtonPromptInstance = Instantiate(repairButtonPrompt, GameObject.FindGameObjectWithTag("UI").transform);
        _repairButtonPromptInstance.SetActive(false);
        
        _repairingImagePromptInstance = Instantiate(repairingImagePrompt, GameObject.FindGameObjectWithTag("UI").transform);
        _repairingImagePromptInstance.SetActive(false);
        
        _materialPromptInstance = Instantiate(needMaterialsPrompt, GameObject.FindGameObjectWithTag("UI").transform);
        _materialPromptInstance.SetActive(false);
    }

    private void Start()
    {
        playerStation.OnPossibleStationSelected += EnableButtonPrompt;
        playerStation.OnPossibleStationDeselected += DisableButtonPrompt;
        playerStation.OnNotTheRightMaterial += PlayNotTheRightMaterial; 
        playerStation.OnRepairTimeChanged += UpdateRadialFill;
        playerStation.OnRepairEnded += () => { _repairingImagePromptInstance.SetActive(false); };
    }

    private void UpdateRadialFill(float time)
    {
        _repairingImagePromptInstance.SetActive(true);
        _repairingImagePromptInstance.GetComponentInChildren<Image>().fillAmount = time / 3;
    }


    private void OnDestroy()
    {
        playerStation.OnPossibleStationSelected -= EnableButtonPrompt;
        playerStation.OnActiveStationDeselected -= DisableButtonPrompt;
        playerStation.OnNotTheRightMaterial -= PlayNotTheRightMaterial;
    }
    
    private void PlayNotTheRightMaterial(Station obj)
    {
        StartCoroutine(ShowMaterialPrompt());
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

    //TODO: Animate this shit
    public IEnumerator ShowMaterialPrompt()
    {
        _materialPromptInstance.SetActive(true);
        yield return new WaitForSeconds(1f);
        _materialPromptInstance.SetActive(false);
    }

    private void Update()
    {
        var camm = Camera.main;
        Vector3 buttonPromptPosition = camm.WorldToScreenPoint(buttonPromptLocation.position);
        Vector3 repairingImagePosition = camm.WorldToScreenPoint(repairPromptLocation.position);
        
        _useButtonPromptInstance.transform.position = buttonPromptPosition;
        _repairButtonPromptInstance.transform.position = buttonPromptPosition;
        _repairingImagePromptInstance.transform.position = repairingImagePosition;
        
        Vector3 materialPromptPosition = camm.WorldToScreenPoint(materialPromptLocation.position);
        _materialPromptInstance.transform.position = materialPromptPosition;
    }
}