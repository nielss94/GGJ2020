using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireRotation : MonoBehaviour
{
    private SphereControls _sphereControls = null;

    private void Awake()
    {
        _sphereControls = FindObjectOfType<SphereControls>();
    }

    private void Update()
    {
        if(_sphereControls)
            transform.Rotate(Vector3.right * (_sphereControls.Move.x * 10 * Time.deltaTime), Space.Self);
    }

}
