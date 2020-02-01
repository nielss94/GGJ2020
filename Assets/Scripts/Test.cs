using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private void Update()
    {
        transform.Rotate(0,0,3 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Quaternion rot = Quaternion.Euler(0, 0, 3 * Time.deltaTime);
        // _rigidbody.MoveRotation(_rigidbody.rotation * rot);
    }
}
