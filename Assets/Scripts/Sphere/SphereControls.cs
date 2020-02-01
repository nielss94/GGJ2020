using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControls : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 3f;
    
    private Vector3 _move;

    private void Awake()
    {
        _move.x = baseSpeed;
    }

    private void Update()
    {
        _move.x = baseSpeed;
        transform.Rotate(_move * (baseSpeed * Time.deltaTime));
    }

    public void Rotate(Vector3 move)
    {
        _move = move;
    }
}
