using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SteeringWheel : Station
{
    [SerializeField] private SphereControls _sphereControls = null;
    [SerializeField] private GameObject[] steeringWheels;
    [SerializeField] private float steerRotation;
    [SerializeField] private float steerSpeed;
    
    private Vector3 _move;
    private float wheelAngle;
    
    private void Update()
    {
        base.Update();
        
        if (IsActive && !IsBroken)
        {
            _sphereControls.Steer(_move);
            // _sphereControls.Accelerate(_move);

            float steerInput = _move.x * steerRotation;
            wheelAngle = Mathf.Lerp(wheelAngle, steerInput, steerSpeed *            Time.deltaTime);
            foreach (GameObject wheel in steeringWheels)
            {
                wheel.transform.localRotation = Quaternion.Euler(wheel.transform.localRotation.x, wheel.transform.localRotation.y + wheelAngle, transform.localRotation.z);
            }
        }
    }

    public override void Terminate()
    {
        base.Terminate();
        _move = Vector3.zero;
        _sphereControls.Steer(_move);
        _sphereControls.Steer(_move);
    }

    public override void ProcessInput(InputValue value)
    {
        var input = value.Get<Vector2>();
        _move = new Vector3(input.x, 0, input.y);
    }
}
