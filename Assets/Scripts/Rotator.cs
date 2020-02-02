using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAngle;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Space space;
    
    private void Update()
    {
        transform.Rotate(rotationAngle * (rotationSpeed * Time.deltaTime), space);    
    }
}
