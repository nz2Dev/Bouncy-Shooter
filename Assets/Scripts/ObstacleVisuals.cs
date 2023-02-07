using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleVisuals : MonoBehaviour {
    
    [SerializeField] private Obstacle obstacle;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        obstacle.OnDetonate += ObstacleOnDetonate;
    }

    private void ObstacleOnDetonate() {
        _animator.SetTrigger("Detonate");
    }
}
