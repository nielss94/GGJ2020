using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SteeringWheel : Station
{
    [SerializeField] private Hovercar _hovercar = null;
    
    private Vector3 _move;
    
    private void Update()
    {
        base.Update();
        
        if (IsActive && !IsBroken)
        {
            _hovercar.Vroom(_move);
        }
    }

    public override void Terminate()
    {
        base.Terminate();
        _move = Vector3.zero;
        _hovercar.Vroom(_move);
    }

    public override void ProcessInput(InputValue value)
    {
        var input = value.Get<Vector2>();
        _move = new Vector3(input.x, 0, input.y);
    }
}
