using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour, IBallShape {

    public event Action OnRadiusChanged;
    public event Action OnChargeBelowCritical;

    public float Radius { get; private set; }
    public bool Charging { get; private set; }

    [SerializeField] private Bullet bullet;
    [SerializeField] private float chargeSpeed = 1;
    [SerializeField] private float startRadius = 5;
    [SerializeField] private float criticalMinRadius = 0.1f;
    [SerializeField] private float bulletOffset = 0.5f;

    private bool _canCharge = true;

    private void Start() {
        Radius = startRadius;
        OnRadiusChanged?.Invoke();

        LoadBullet();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartCharging();
        }

        if (Input.GetMouseButtonUp(0)) {
            ReleaseCharge();
        }

        Charge();
    }

    private void StartCharging() {
        if (_canCharge) {
            LoadBullet();
            Charging = true;
        }
    }

    private void Charge() {
        if (_canCharge && Charging) {
            var chargeRadiusDelta = chargeSpeed * Time.deltaTime;
            
            // decrease player radius
            Radius -= chargeRadiusDelta;
            OnRadiusChanged?.Invoke();

            if (Radius <= criticalMinRadius) {
                _canCharge = false;
                OnChargeBelowCritical?.Invoke();

                Charging = false;
            } else {
                // increase bullet radius
                bullet.ChangeRadius(chargeRadiusDelta);
            }
        }
    }

    private void ReleaseCharge() {
        if (_canCharge && Charging) {
            Charging = false;

            bullet.Shoot(transform.forward);
        }
    }

    private void LoadBullet() {
        var initialDistanceToBullet = startRadius + bulletOffset;
        bullet.SetPosition(transform.position + transform.forward * initialDistanceToBullet);
        bullet.SetRadiusToMinimum();
    }

}
