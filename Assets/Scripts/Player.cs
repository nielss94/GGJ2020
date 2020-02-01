using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Rigidbody _platformRigidbody = null;
    
    private Rigidbody _rigidbody = null;
    private Vector3 _move = default;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var nextPos = _rigidbody.position + transform.TransformDirection(_move * (moveSpeed * Time.fixedDeltaTime));
        _rigidbody.MovePosition(nextPos);
    }

    public void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        _move = new Vector3(input.x, 0, input.y);
    }
    
}
