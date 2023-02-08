using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour, IBallShape {

    public event Action OnRadiusChanged;
    public event Action OnChargeBelowCritical;
    public event Action OnPathAvailabilityUpdated;
    public event Action OnIsMovingChanged;

    public float Radius { get; private set; }
    public bool IsCharging { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsPathAvailable { get; private set; }

    [SerializeField] private Bullet bullet;
    [SerializeField] private LayerMask obstaclesLayerMask;
    [SerializeField] private float chargeSpeed = 1;
    [SerializeField] private float startRadius = 5;
    [SerializeField] private float criticalMinRadius = 0.1f;
    [SerializeField] private float bulletOffset = 0.5f;
    [SerializeField] private float movementSpeed = 2f;

    private bool _canCharge = true;
    private float _lastPathCheckTime;

    private void Start() {
        Radius = startRadius;
        OnRadiusChanged?.Invoke();

        LoadBullet();
        CheckPathAvailability();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartCharging();
        }

        if (Input.GetMouseButtonUp(0)) {
            ReleaseCharge();
        }

        Charge();

        var pathCheckInterval = 0.25f;
        if (_lastPathCheckTime + pathCheckInterval < Time.time) {
            CheckPathAvailability();
            _lastPathCheckTime = Time.time;
        }

        Movement();
    }

    private void Movement() {
        if (IsMoving) {
            transform.position += transform.forward * (Time.deltaTime * movementSpeed);
        }
    }

    private void StartCharging() {
        if (GameManager.Instance.IsPlayingState && _canCharge && !IsMoving) {
            LoadBullet();
            IsCharging = true;
        }
    }

    private void Charge() {
        if (GameManager.Instance.IsPlayingState && _canCharge && IsCharging) {
            var chargeRadiusDelta = chargeSpeed * Time.deltaTime;
            
            // decrease player radius
            Radius -= chargeRadiusDelta;
            OnRadiusChanged?.Invoke();

            if (Radius <= criticalMinRadius) {
                _canCharge = false;
                OnChargeBelowCritical?.Invoke();

                IsCharging = false;
            } else {
                // increase bullet radius
                bullet.ChangeRadius(chargeRadiusDelta);
            }
        }
    }

    private void ReleaseCharge() {
        if (GameManager.Instance.IsPlayingState && _canCharge && IsCharging) {
            IsCharging = false;

            bullet.Shoot(transform.forward);
        }
    }

    private void LoadBullet() {
        var initialDistanceToBullet = startRadius + bulletOffset;
        bullet.SetPosition(transform.position + transform.forward * initialDistanceToBullet);
        bullet.SetRadiusToMinimum();
        bullet.gameObject.SetActive(true);
    }

    private void HideBullet() {
        bullet.gameObject.SetActive(false);
    }

    private void CheckPathAvailability() {
        IsPathAvailable = !Physics.SphereCast(transform.position, Radius, transform.forward, out var hitInfo, float.PositiveInfinity, obstaclesLayerMask);
        OnPathAvailabilityUpdated?.Invoke();

        if (_canCharge && !IsMoving && IsPathAvailable) {
            IsMoving = true;
            OnIsMovingChanged?.Invoke();
            HideBullet();
        }
    }

}
