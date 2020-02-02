using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Railing : Station
{
    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("Holding the railing");
    }

    public override void Terminate()
    {
        base.Terminate();
    }

    public override void ProcessInput(InputValue value)
    {
    }
}
