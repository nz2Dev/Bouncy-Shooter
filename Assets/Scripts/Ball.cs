using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public event Action OnRadiusChanged;

    public float Radius => _radius;

    [SerializeField] private float minRadius = 0.1f;
    [SerializeField] private float maxRadius = 5;
    
    private float _radius = 1f;

    public void SetRadiusToMinimum() {
        SetRadius(minRadius);
    }

    public void SetRadius(float newRadius) {
        _radius = Mathf.Clamp(newRadius, minRadius, maxRadius);
        OnRadiusChanged?.Invoke();
    }

    public bool ChangeRadius(float radiusDelta) {
        var changesRadius = _radius + radiusDelta;
        _radius = Mathf.Clamp(changesRadius, minRadius, maxRadius);
        OnRadiusChanged?.Invoke();
        return _radius == changesRadius;
    }

}
