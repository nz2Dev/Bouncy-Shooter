using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public event Action OnOpened;
    public event Action OnPlayerEntered;

    public bool IsOpen { get; private set; }
    public bool IsPlayerEntered { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private float openDistance = 5f;

    private void Update() {
        if (!IsPlayerEntered) {
            // player has not entered yet
            var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (!IsOpen) {
                // door is not opened yet
                if (distanceToPlayer < openDistance) {
                    IsOpen = true;
                    OnOpened?.Invoke();
                }
            } else {
                // door is opened now
                if (distanceToPlayer < 1.0f) {
                    // player has entered
                    IsPlayerEntered = true;
                    OnPlayerEntered?.Invoke();
                }
            }
        }
    }

}
