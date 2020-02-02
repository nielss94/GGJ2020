using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StationManager : MonoBehaviour
{
    public static event Action<int> OnBrokenStationsChanged = delegate(int i) { };
    public static StationManager Instance { get; private set; }

    [SerializeField]
    private List<Station> stations = new List<Station>();

    private int _brokenStation = 0;

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

        foreach (Station station in stations)
        {
            station.OnIsBroken += IncreaseBroken;
            station.OnIsRepaired += DecreaseBroken;
        }
    }

    private void Start()
    {
        _brokenStation = stations.Count(station => station.IsBroken);
        StartCoroutine(SetBrokenStation());
    }

    private IEnumerator SetBrokenStation()
    {
        yield return new WaitForSeconds(0.2f);
        OnBrokenStationsChanged.Invoke(_brokenStation);
    }

    private void DecreaseBroken(Station station)
    {
        _brokenStation--;
        OnBrokenStationsChanged.Invoke(_brokenStation);
    }

    private void IncreaseBroken(Station station)
    {
        _brokenStation++;
        OnBrokenStationsChanged.Invoke(_brokenStation);
    }

    public void DamageRandomStation()
    {
        List<Station> activeStations = stations.FindAll(s => s.IsBroken == false);

        if (activeStations.Count <= 0) return;

        int random = Random.Range(0, activeStations.Count);

        activeStations[random].Damage();

    }
}