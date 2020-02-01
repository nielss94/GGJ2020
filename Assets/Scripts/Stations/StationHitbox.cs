using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationHitbox : MonoBehaviour
{
    [SerializeField]
    private Station station;

    public Station GetStation()
    {
        return station;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStation playerStation))
        {
            playerStation.SetPossibleStation(station);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerStation playerStation))
        {
            playerStation.RemovePossibleStation();
        }
    }
}
