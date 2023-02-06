using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public event Action OnRadiusChanged;

    public float Radius => radius;

    [SerializeField] private float radius = 1f;
    [SerializeField] private float minRadius = 0.1f;
    [SerializeField] private float maxRadius = 5;

    private void Start() {
        SetRadius(radius);
    }

    public void SetRadiusToMinimum() {
        SetRadius(minRadius);
    }

    public void SetRadius(float newRadius) {
        radius = Mathf.Clamp(newRadius, minRadius, maxRadius);
        OnRadiusChanged?.Invoke();
    }

    public bool ChangeRadius(float radiusDelta) {
        var changesRadius = radius + radiusDelta;
        radius = Mathf.Clamp(changesRadius, minRadius, maxRadius);
        OnRadiusChanged?.Invoke();
        return radius == changesRadius;
    }

}
