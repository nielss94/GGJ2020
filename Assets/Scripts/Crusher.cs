using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    [SerializeField] private ConveyorBelt _conveyorBelt = null;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            _conveyorBelt.AddToBelt(resource.part);
            Destroy(resource.gameObject);
            AudioManager.instance.PlayPickupSounds(0);
        }
    }
}
