using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : Station
{
    [SerializeField]
    private float rotateSpeed = 2f;
    [SerializeField]
    private float maxRotation = 0.67f;
    [SerializeField]
    private float minRotation = 200f;
    [SerializeField]
    private bool reverseMovement = false;

    private float _yMovement;
    private float _xRotation;

    private void Update()
    {
        base.Update();
        
        float turnSpeed = rotateSpeed * Time.deltaTime;
        float rotate = -(_yMovement * turnSpeed);

        if (reverseMovement)
        {
            rotate = -rotate;
        }

        if (rotate > 0 && transform.eulerAngles.y < maxRotation || rotate < 0 && transform.eulerAngles.y > minRotation)
            transform.Rotate(Vector3.forward * rotate);
    }

    public override void ProcessInput(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _yMovement = input.y;
    }
}