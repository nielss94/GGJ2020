using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform floorDetection = null;
    
    private bool _canMove = true;
    private bool _isOnPlatform = false;
    
    private PlayerStation _playerStation = null;
    private Rigidbody _rigidbody = null;
    private Vector3 _move = default;
    private Transform _playerSpawn = null;
    
    private void Awake()
    {
        _playerStation = GetComponent<PlayerStation>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _playerSpawn = GameObject.Find("PlayerSpawn").transform;
        transform.position = _playerSpawn.position;
        transform.rotation = Quaternion.identity;
        transform.SetParent(FindObjectOfType<CenterToHovercar>().transform);

        _playerStation.OnActiveStationSelected += DisableMovement;
        _playerStation.OnActiveStationDeselected += EnableMovement;
    }
    
    private void Update()
    {
        // if (_canMove)
        // {
        //     transform.Translate(_move * (moveSpeed * Time.deltaTime), Space.Self);
        // }

        

        _rigidbody.isKinematic = _isOnPlatform;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            var nextPos = _rigidbody.position + transform.TransformDirection(_move * (moveSpeed * Time.fixedDeltaTime));
            _rigidbody.MovePosition(nextPos);
        }
        
        _isOnPlatform = Physics.SphereCast(floorDetection.position, .05f, Vector3.down, out var hit, 1,
            LayerMask.GetMask("Platform"));
    }

    private void EnableMovement(Station station)
    {
        _canMove = true;
    }

    private void DisableMovement(Station station)
    {
        _canMove = false;
    }

    public void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        _move = new Vector3(input.x, _move.y, input.y);
    }
}
