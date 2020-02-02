using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform floorDetection = null;
    [SerializeField] private float respawnTime = 3f;
    
    private bool _canMove = true;
    public bool CanMove => _canMove;

    private bool _isOnPlatform = false;
    private PlayerPickUp _playerPickUp = null;
    private PlayerStation _playerStation = null;
    private Rigidbody _rigidbody = null;
    private Vector3 _move = default;
    public Vector3 MoveInput => _move;
    
    private Transform _playerSpawn = null;
    private bool _isDead = false;
    private CenterToHovercar _centerToHovercar;

    private void Awake()
    {
        _playerPickUp = GetComponentInChildren<PlayerPickUp>();
        _centerToHovercar = FindObjectOfType<CenterToHovercar>();
        _playerStation = GetComponent<PlayerStation>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _playerStation.OnActiveStationSelected += DisableMovement;
        _playerStation.OnActiveStationDeselected += EnableMovement;

        Init();
    }

    private void Init()
    {
        _playerSpawn = GameObject.Find("PlayerSpawn").transform;
        transform.position = _playerSpawn.position;
        transform.rotation = _playerSpawn.rotation;

        _isDead = false;
    }
    
    private void Update()
    {
        _rigidbody.isKinematic = _isOnPlatform;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Vector3 nextPos = _rigidbody.position + _move * (moveSpeed * Time.deltaTime);
            _rigidbody.MovePosition(nextPos);
        }
        
        _isOnPlatform = Physics.Raycast(floorDetection.position, Vector3.down, out var hit, .1f,
            LayerMask.GetMask("Platform"));
    }

    public void OnUse()
    {
        _playerPickUp.PickUp();
    }
    
    public void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        _move = new Vector3(input.x, 0, input.y);
    }

    private void EnableMovement(Station station)
    {
        _canMove = true;
    }

    private void DisableMovement(Station station)
    {
        _canMove = false;
    }

    private IEnumerator Die()
    {
        DisableMovement(null);
        transform.SetParent(null);
        _isDead = true;
        
        yield return new WaitForSeconds(respawnTime);
        EnableMovement(null);
        Init();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            if (!_isDead)
            {
                StartCoroutine(Die());
            }
        }
    }
}
