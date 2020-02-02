using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StationManager : MonoBehaviour
{
    public static StationManager Instance { get; private set; }
    
    [SerializeField] private List<Station> stations = new List<Station>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            stations = FindObjectsOfType<Station>().ToList();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DamageRandomStation()
    {
        List<Station> activeStations = stations.FindAll(s => s.IsBroken == false);
    
        if (activeStations.Count <= 0) return;
        
        int random = Random.Range(0, activeStations.Count);
        
        activeStations[random].Damage();

        AudioManager.instance.IncreaseDanger();
    }
}