using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (_player.MoveInput != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(_player.MoveInput, Vector3.up);
            transform.localRotation = rotation;
        }
    }
}
