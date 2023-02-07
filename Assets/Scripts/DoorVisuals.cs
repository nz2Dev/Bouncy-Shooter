using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVisuals : MonoBehaviour {

    [SerializeField] private Door door;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        door.OnOpened += DoorOnOpened;
    }

    private void DoorOnOpened() {
        _animator.SetTrigger("OpenDoor");
    }
}
