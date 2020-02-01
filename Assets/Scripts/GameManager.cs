using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameStarted = delegate {  };
    
    private PlayerInputManager _playerInputManager;
    private PlayerJoinedUI _playerJoinedUi;
    private int _playersJoined = 0;

    private bool _gameStarted = false;
    public bool GameStarted => _gameStarted;

    private void Awake()
    {
        _playerJoinedUi = GetComponent<PlayerJoinedUI>();
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void OnPlayerJoined()
    {
        _playerJoinedUi.SetPlayerActive(_playersJoined);
        _playersJoined++;

        if (_playersJoined == _playerInputManager.maxPlayerCount)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        _gameStarted = true;
        StartCoroutine(InvokeGameStart());
    }

    private IEnumerator InvokeGameStart()
    {
        yield return new WaitForSeconds(0.25f);
        OnGameStarted.Invoke();
    }
}