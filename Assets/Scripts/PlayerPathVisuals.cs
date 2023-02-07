using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathVisuals : MonoBehaviour {

    [SerializeField] private Player player;
    [SerializeField] private Color pathAvailableColor;
    [SerializeField] private Color pathUnavailableColor;
    [SerializeField] private LineRenderer radiusVisualizationLine;
    [SerializeField] private LineRenderer criticalRadiusVisualizationLine;

    private void Awake() {
        player.OnRadiusChanged += PlayerOnRadiusChanged;
        player.OnPathAvailabilityUpdated += PlayerOnPathAvailabilityChanged;
    }

    private void PlayerOnRadiusChanged() {
        radiusVisualizationLine.widthMultiplier = player.Radius * 2f;
    }

    private void PlayerOnPathAvailabilityChanged() {
        var pathColor = player.IsPathAvailable ? pathAvailableColor : pathUnavailableColor;
        radiusVisualizationLine.startColor = pathColor;
        radiusVisualizationLine.endColor = pathColor;

        criticalRadiusVisualizationLine.gameObject.SetActive(!player.IsPathAvailable);
    }

}
