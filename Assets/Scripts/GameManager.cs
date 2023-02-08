using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public enum State {
        Playing,
        GameOver
    }

    public static GameManager Instance { get; private set; }

    public event Action OnStateChanged;

    public State CurrentState { get; private set; }

    [SerializeField] private Player targetPlayer;
    [SerializeField] private Door targetDoor;

    private void Awake() {
        Instance = this;
        targetPlayer.OnChargeBelowCritical += PlayerOnChargeBelowCritical;
        targetDoor.OnPlayerEntered += DoorOnPlayerEntered;
    }   

    private void Start() {
        GameStart();
    }

    private void PlayerOnChargeBelowCritical() {
        GameOver();
    }

    private void DoorOnPlayerEntered() {
        GameOver();
    }

    private void GameStart() {
        CurrentState = State.Playing;
        OnStateChanged?.Invoke();
    }

    private void GameOver() {
        CurrentState = State.GameOver;
        OnStateChanged?.Invoke();
    }
    
}
