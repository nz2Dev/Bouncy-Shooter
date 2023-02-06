using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IBallShape {
    
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
    
    private void FixedUpdate() {
        if (Flying) {
            var flyDelta = Time.fixedDeltaTime * speedUnitsPerSecond * _flyDirection;
            _rigidbody.MovePosition(_rigidbody.position + flyDelta);
        }
    }

    public void SetPosition(Vector3 position) {
        _rigidbody.position = position;
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

    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody != null && other.attachedRigidbody.TryGetComponent(out Obstacle obstacle)) {
            Flying = false;
            Debug.Log("On Obstacle Triggered");
        }
    }

}
