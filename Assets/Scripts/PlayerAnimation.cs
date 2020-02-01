using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator = null;
    private Player _player = null;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _animator.SetBool("Walking", _player.MoveInput != Vector3.zero);
    }
}
