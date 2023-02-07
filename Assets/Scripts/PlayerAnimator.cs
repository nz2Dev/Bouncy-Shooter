using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour {
    
    [SerializeField] private Player player;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        player.OnIsMovingChanged += PlayerOnIsMovingChanged;
    }

    private void PlayerOnIsMovingChanged() {
        _animator.SetBool("Movement", player.IsMoving);
    }

}
