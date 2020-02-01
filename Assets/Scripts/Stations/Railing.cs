using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Railing : Station
{
    public override void Initialize()
    {
        Debug.Log("Holding the railing");
    }

    public override void Terminate()
    {
        throw new System.NotImplementedException();
    }

    public override void ProcessInput(InputValue value)
    {
    }
}
