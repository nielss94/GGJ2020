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

    private void Start()
    {
        GameObject tr = GameObject.Find("PlayerSpawn");
        transform.SetParent(tr.transform);
        transform.localPosition = Vector3.zero;
    }


    private void Update()
    {
        transform.Translate(_move * (moveSpeed * Time.deltaTime), Space.Self);
    }

    public void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        _move = new Vector3(input.x, 0, input.y);
    }
    
}
