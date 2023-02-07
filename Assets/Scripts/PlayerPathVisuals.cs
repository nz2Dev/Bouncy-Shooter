using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathVisuals : MonoBehaviour {

    [SerializeField] private Player player;

    private LineRenderer _lineRenderer;

    private void Awake() {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        player.OnRadiusChanged += PlayerOnRadiusChanged;
    }

    private void PlayerOnRadiusChanged() {
        _lineRenderer.widthMultiplier = player.Radius * 2f;
    }
}
