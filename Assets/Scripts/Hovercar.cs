using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hovercar : MonoBehaviour
{
    public float hoverForce = 9.0f;
    public float hoverHeight = 2.0f;
    public float forwardAccelleration = 100.0f;
    public float backwardAccelleration = 25.0f;
    public float turnStrength = 10.0f;
    public LayerMask raycastFilter;
    public GameObject[] hoverPoints;

    private Rigidbody _rigidBody;
    private float _deadZone = 0.1f;
    private float _currentThrust = 0.0f;
    private float _currentTurnRate = 0.0f;

    private Vector3 _move;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        InitAudio();
    }

    private void Update()
    {
        // Thrust
        _currentThrust = 0.0f;
        if (_move.z > _deadZone)
        {
            _currentThrust = _move.z * forwardAccelleration;
        }
        else if (_move.z < -_deadZone)
        {
            _currentThrust = _move.z * backwardAccelleration;
        }

       // Steering
       _currentTurnRate = 0.0f;
        if (Mathf.Abs(_move.x) > _deadZone)
        {
            _currentTurnRate = _move.x;
        }
    }

    private void FixedUpdate()
    {
        // Hover force
        RaycastHit hit;
        foreach (GameObject point in hoverPoints)
        {
            if (Physics.Raycast(point.transform.position, -Vector3.up, out hit, hoverHeight, raycastFilter))
            {
                _rigidBody.AddForceAtPosition(Vector3.up * hoverForce * 
                    (1.0f - (hit.distance / hoverHeight)), point.transform.position);
            }
            else
            {
                if (transform.position.y > point.transform.position.y)
                {
                    _rigidBody.AddForceAtPosition(point.transform.up * 0.5f * hoverForce, 
                        point.transform.position);
                }
                else
                {
                    _rigidBody.AddForceAtPosition(point.transform.up * 0.5f * -hoverForce,
                        point.transform.position);
                }
            }
        }

        // Thrust
        if (Mathf.Abs(_currentThrust) > 0)
        {
            _rigidBody.AddForce(transform.forward * _currentThrust);
        }

        // Turn
        if (_currentTurnRate != 0)
        {
            _rigidBody.AddRelativeTorque(Vector3.up * _currentTurnRate * turnStrength);
        }
    }

    public void Vroom(Vector3 move)
    {
        _move = move;
    }


    private void InitAudio()
    {
      //  AudioManager.instance.PlayEngineSounds(0);
    }
}
