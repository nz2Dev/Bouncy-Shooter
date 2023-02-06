using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IBallShape {

    public event Action OnRadiusChanged;

    public float Radius { get; private set; }
    public bool Charging { get; private set; }

    [SerializeField] private Bullet bullet;
    [SerializeField] private float radiusUnitsPerSeconds = 1;
    [SerializeField] private float minRadius = 0.1f;
    [SerializeField] private float maxRadius = 5;
    [SerializeField] private float startRadius = 5;
    [SerializeField] private float bulletOffset = 0.5f;

    private void Start() {
        Radius = startRadius;
        OnRadiusChanged?.Invoke();

        bullet.SetRadiusToMinimum();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartCharging();
        }

        if (Input.GetMouseButtonUp(0)) {
            ReleaseCharge();
        }

        if (Charging) {
            Charge();
        }
    }

    private void StartCharging() {
        Charging = true;

        bullet.SetRadiusToMinimum();
        var initialDistanceToBullet = Radius + bullet.Radius + bulletOffset;
        bullet.transform.position = transform.position + transform.forward * initialDistanceToBullet;
    }

    private void Charge() {
        var radiusDelta = radiusUnitsPerSeconds * Time.deltaTime;
        ChangePlayerRadius(-radiusDelta);
        bullet.ChangeRadius(radiusDelta);
    }

    private void ReleaseCharge() {
        Charging = false;

        SetPlayerRadius(startRadius);
        bullet.SetRadiusToMinimum();
    }

    private void SetPlayerRadius(float newRadius) {
        Radius = Mathf.Clamp(newRadius, minRadius, maxRadius);
        OnRadiusChanged?.Invoke();
    }

    private bool ChangePlayerRadius(float radiusDelta) {
        var changesRadius = Radius + radiusDelta;
        Radius = Mathf.Clamp(changesRadius, minRadius, maxRadius);
        OnRadiusChanged?.Invoke();
        return Radius == changesRadius;
    }

}
