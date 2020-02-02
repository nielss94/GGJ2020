using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowersDestroyedUI : MonoBehaviour
{
    [SerializeField]
    private string towerDestroyedText = "Towers destroyed:";

    private TextMeshProUGUI _towerDestroyedUi;
    
    private void Awake()
    {
        _towerDestroyedUi = GetComponent<TextMeshProUGUI>();
        TowerDestroyedManager.OnTowerDestroyed += ChangeTowerDestroyText;
    }

    private void Start()
    {
        ChangeTowerDestroyText(0);
    }

    private void ChangeTowerDestroyText(int towersDestroyed)
    {
        _towerDestroyedUi.text = $"{towerDestroyedText} {towersDestroyed}";
    }
}
