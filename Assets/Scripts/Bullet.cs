using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {
    
    public event Action OnRadiusChanged;

    public float Radius { get; private set; }
    public bool Flying { get; private set; }

    [SerializeField] private float speedUnitsPerSecond = 4f;
    [SerializeField] private float minRadius = 0.1f;
    [SerializeField] private float maxRadius = 5;
    
    private Rigidbody _rigidbody;
    private Vector3 _flyDirection;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.isKinematic = true;
    }
    
    private void FixedUpdate() {
        if (Flying) {
            _rigidbody.MovePosition(Time.fixedDeltaTime * speedUnitsPerSecond * _flyDirection);
        }
    }

    public void SetRadiusToMinimum() {
        SetRadius(minRadius);
    }

    public void SetRadius(float newRadius) {
        Radius = Mathf.Clamp(newRadius, minRadius, maxRadius);
        OnRadiusChanged?.Invoke();
    }

    public bool ChangeRadius(float radiusDelta) {
        var changesRadius = Radius + radiusDelta;
        Radius = Mathf.Clamp(changesRadius, minRadius, maxRadius);
        OnRadiusChanged?.Invoke();
        return Radius == changesRadius;
    }

    public void Shoot(Vector3 direction) {
        _flyDirection = direction;
        Flying = true;
    }

}
