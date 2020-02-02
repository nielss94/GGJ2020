using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControls : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 3f;
    [SerializeField] private float accelerationMultiplier = 1.5f;
	[SerializeField] private float steerSpeed = 10f;

    private Vector3 _move;
    public Vector3 Move => _move;
    

    private void Awake()
    {
        _move.x = -baseSpeed;
    }

    private void Update()
    {
        transform.Rotate(_move * Time.deltaTime, Space.World);
    }

    public void Steer(Vector3 move)
    {
        _move.y = move.x * -steerSpeed;
    }

    public void Accelerate(Vector3 move)
    {
        if (move.z > 0)
        {
            _move.x = -baseSpeed * accelerationMultiplier;
        }
        else
        {
            _move.x = -baseSpeed;
        }
    }
}