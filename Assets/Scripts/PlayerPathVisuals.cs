using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathVisuals : MonoBehaviour {

    [SerializeField] private Player player;
    [SerializeField] private Color pathAvailableColor;
    [SerializeField] private Color pathUnavailableColor;

    private LineRenderer _lineRenderer;

    private void Awake() {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        player.OnRadiusChanged += PlayerOnRadiusChanged;
        player.OnPathAvailabilityUpdated += PlayerOnPathAvailabilityChanged;
    }

    private void PlayerOnRadiusChanged() {
        _lineRenderer.widthMultiplier = player.Radius * 2f;
    }

    private void PlayerOnPathAvailabilityChanged() {
        var pathColor = player.IsPathAvailable ? pathAvailableColor : pathUnavailableColor;
        _lineRenderer.startColor = pathColor;
        _lineRenderer.endColor = pathColor;
    }

}
