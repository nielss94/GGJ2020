using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterToHovercar : MonoBehaviour
{
    private Hovercar _hovercar;

    private void Awake()
    {
        _hovercar = FindObjectOfType<Hovercar>();
    }

    private void Update()
    {
        transform.position = _hovercar.transform.position;
        transform.rotation = _hovercar.transform.rotation;
    }
}
