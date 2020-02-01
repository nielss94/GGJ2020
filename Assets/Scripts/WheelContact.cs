using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelContact : MonoBehaviour
{
    public float maxExtension;
    public float wheelRadius;
    public GameObject wheelMesh;
    
    private float _currentExtension;
    private Vector3 _initialWheelPosition;

    private void Awake()
    {
        _initialWheelPosition = wheelMesh.transform.position;
    }

    private void Update()
    {
        wheelMesh.transform.position = _initialWheelPosition;

        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxExtension + wheelRadius))
        {
            Debug.Log("HIT!");
            _currentExtension = hit.distance - wheelRadius;
        }
        else
        {
            _currentExtension = maxExtension;
        }

        wheelMesh.transform.localPosition = new Vector3(0, -_currentExtension, 0);
    }
}
