using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState GameState;
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GridManagerStart();
                break;

            case GameState.SpawnPlayer:
                UnitManager.Instance.InitUnitManager();
                UnitManager.Instance.SpawnPlayer();
                break;

            case GameState.SpawnUtilities:
                UnitManager.Instance.SpawnUtilites();
                break;

            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;

            case GameState.GameStart:
                StartGame();
                break;

            case GameState.PlayerDied:
                break;

            case GameState.GameOver:
                break;

            case GameState.GameWin:
                break;

            case GameState.GameEdit:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    public void StartGame()
    {

    }
}


public enum GameState
{
    GenerateGrid,
    SpawnPlayer,
    SpawnUtilities,
    SpawnEnemies,
    GameStart,
    PlayerDied,
    GameOver,
    GameWin,
    GameEdit
}