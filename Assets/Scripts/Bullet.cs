using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IBallShape {
    
    public event Action OnRadiusChanged;

    public float Radius { get; private set; }
    public bool Flying { get; private set; }

    [SerializeField] private LayerMask detonationMask;
    [SerializeField] private float hitWaveDistance = 1f;
    [SerializeField] private float speedUnitsPerSecond = 4f;
    [SerializeField] private float minRadius = 0.1f;
    [SerializeField] private float maxRadius = 5;
    
    private Rigidbody _rigidbody;
    private Vector3 _flyDirection;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
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

    private void FixedUpdate() {
        if (Flying) {
            var flyDelta = Time.fixedDeltaTime * speedUnitsPerSecond * _flyDirection;
            _rigidbody.MovePosition(_rigidbody.position + flyDelta);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody != null && other.attachedRigidbody.TryGetComponent(out Obstacle obstacle)) {
            Flying = false;
            Detonate();
        }
    }

    private void Detonate() {
        var detonationRadius = Radius + hitWaveDistance;
        var nearObstaclesHits = Physics.SphereCastAll(transform.position, detonationRadius, Vector3.up, detonationMask);
        foreach (var obstacleHit in nearObstaclesHits) {
            if (obstacleHit.rigidbody != null && obstacleHit.rigidbody.TryGetComponent(out Obstacle obstacle)) {
                obstacle.DestroySelf();
            }
        }
    }
    

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, Radius + hitWaveDistance);
    }
#endif

}
