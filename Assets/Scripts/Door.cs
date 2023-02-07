using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public event Action OnOpened;

    public bool IsOpen { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private float openDistance = 5f;

    private void Update() {
        if (!IsOpen) {
            var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < openDistance) {
                IsOpen = true;
                OnOpened?.Invoke();
            }
        }
    }

}
