﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    [SerializeField] private ConveyorBelt _conveyorBelt = null;
    [SerializeField] private GameObject conveyorPartSample = null;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            _conveyorBelt.AddToBelt(conveyorPartSample);
            Destroy(resource.gameObject);
            AudioManager.instance.PlayPickupSounds(0);
        }
    }
}
