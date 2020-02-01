using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameStarted = delegate {  };
    
    private PlayerInputManager _playerInputManager;
    private int _playersJoined = 0;

    private bool _gameStarted = false;
    public bool GameStarted => _gameStarted;

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void OnPlayerJoined()
    {
        _playersJoined++;

        if (_playersJoined == _playerInputManager.maxPlayerCount)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        _gameStarted = true;
        OnGameStarted.Invoke();
    }
}