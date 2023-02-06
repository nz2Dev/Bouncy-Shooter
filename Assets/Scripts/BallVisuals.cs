using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVisuals : MonoBehaviour {
    
    [SerializeField] private Ball ball;

    private void Awake() {
        ball.OnRadiusChanged += OnBallRadiusChanged;
    }

    private void OnBallRadiusChanged() {
        SyncScaleWithRadius();
    }

    private void SyncScaleWithRadius() {
        // default unit sphere mesh with origin at the center will cover half of unit in the scene (in radius) when scale is one
        // that's why we should multiply scale twise to represent the proper amount of units that float radius contains
        // in other words: 1 radius = 2 * scale of half unit sphere
        transform.localScale = Vector3.one * (ball.Radius * 2f);
    }
}
