using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Station : MonoBehaviour
{
    [SerializeField]
    private bool isHoldInteraction = false;

    public bool GetIsHoldInteraction()
    {
        return isHoldInteraction;
    }

    public abstract void Execute();
}
