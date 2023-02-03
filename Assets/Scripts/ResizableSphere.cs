using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizableSphere : MonoBehaviour {

    [SerializeField] private GameObject geometry;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float minRadius = 0.1f;
    [SerializeField] private float maxRadius = 5;

    public float Radius => radius;

    private void Start() {
        SetRadius(radius);
    }

    public void SetRadiusToMinimum() {
        SetRadius(minRadius);
    }

    public void SetRadius(float newRadius) {
        radius = Mathf.Clamp(newRadius, minRadius, maxRadius);
        SyncScaleWithRadius();
    }

    public bool ChangeRadius(float radiusDelta) {
        var changesRadius = radius + radiusDelta;
        radius = Mathf.Clamp(changesRadius, minRadius, maxRadius);
        SyncScaleWithRadius();
        return radius == changesRadius;
    }

    private void SyncScaleWithRadius() {
        // default unit sphere mesh with origin at the center will cover half of unit in the scene (in radius) when scale is one
        // that's why we should multiply scale twise to represent the proper amount of units that float radius contains
        // in other words: 1 radius = 2 * scale of half unit sphere
        geometry.transform.localScale = Vector3.one * (radius * 2f);
    }

}
