using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThingsBrokenUI : MonoBehaviour
{
    [SerializeField]
    private string stationsDestroyedText = "Stations destroyed:";

    private TextMeshProUGUI _thingsBrokenUi;
    
    private void Awake()
    {
        _thingsBrokenUi = GetComponent<TextMeshProUGUI>();
        StationManager.OnBrokenStationsChanged += ChangeBrokenText;
    }

    private void Start()
    {
        ChangeBrokenText(0);
    }

    private void ChangeBrokenText(int stationsDestroyed)
    {
        _thingsBrokenUi.text = $"{stationsDestroyedText} {stationsDestroyed}";
    }
}
