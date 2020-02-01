﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private GameObject _hovering = null;
    private GameObject _currentPickUp = null;
    
    private List<GameObject> _hoverings = new List<GameObject>();
    private ConveyorBelt _conveyorBelt = null;

    private void Awake()
    {
        _conveyorBelt = FindObjectOfType<ConveyorBelt>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ConveyorBeltPart"))
        {
            _hoverings.Add(other.gameObject);
            _hovering = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_hoverings.Contains(other.gameObject))
        {
            _hoverings.Remove(other.gameObject);
            if (_hoverings.Count > 0)
            {
                _hovering = _hoverings[0];
            }
            else
            {
                _hovering = null;
            }
        }
    }

    public void PickUp()
    {
        if (_hovering != null && _currentPickUp == null)
        {
            _hovering.transform.SetParent(transform);
            _conveyorBelt.RemoveFromBelt(_hovering);
            _currentPickUp = _hovering;
        }
    }
}