using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControls : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 3f;

    private Vector3 _move;
    public Vector3 Move => _move;
    

    private void Awake()
    {
        _move.x = -(baseSpeed / 2);
    }

    private void Update()
    {
        transform.Rotate(_move * Time.deltaTime, Space.World);
    }

    public void Rotate(Vector3 move)
    {
        _move.y = move.x * baseSpeed * -1;
    }
}