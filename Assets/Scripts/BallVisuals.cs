using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVisuals : MonoBehaviour {
    
    [SerializeField] private GameObject ballGameObject;

    private IBallShape _ballShape;

    private void Awake() {
        var _ballShape = ballGameObject.GetComponent<IBallShape>();
        if (_ballShape == null) {
            Debug.LogError("ballGameObject does not have component with IBallShape interface");
        }

        _ballShape.OnRadiusChanged += OnBallRadiusChanged;
    }

    private void OnBallRadiusChanged() {
        SyncScaleWithRadius();
    }

    private void SyncScaleWithRadius() {
        // default unit sphere mesh with origin at the center will cover half of unit in the scene (in radius) when scale is one
        // that's why we should multiply scale twise to represent the proper amount of units that float radius contains
        // in other words: 1 radius = 2 * scale of half unit sphere
        transform.localScale = Vector3.one * (_ballShape.Radius * 2f);
    }
}
