using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private float pickupRange = 1f;
    private Resource _pickUpResource = null;


    private void Update()
    {
        if (!Physics.Raycast(transform.position, transform.forward, out var hit, pickupRange,
            LayerMask.GetMask("ConveyorBeltPart"))) return;
        
        if (!TryGetComponent(out Resource resource)) return;
            
        if (_pickUpResource != resource)
        {
            _pickUpResource = resource;
        }
    }

    public void OnUse()
    {
        print("ASD");
    }
}
