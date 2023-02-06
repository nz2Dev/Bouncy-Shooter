using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour, IBallShape {

    public event Action OnRadiusChanged;

    public float Radius { get; private set; }
    public bool Charging { get; private set; }

    [SerializeField] private Bullet bullet;
    [SerializeField] private float chargeSpeed = 1;
    [SerializeField] private float startRadius = 5;
    [SerializeField] private float bulletOffset = 0.5f;

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

        if (Charging) {
            Charge();
        }
    }

    private void StartCharging() {
        Charging = true;

        LoadBullet();
    }

    private void Charge() {
        var chargeRadiusDelta = chargeSpeed * Time.deltaTime;
        
        // decrease player radius
        Radius -= chargeRadiusDelta;
        OnRadiusChanged?.Invoke();

        // increase bullet radius
        bullet.ChangeRadius(chargeRadiusDelta);
    }

    private void ReleaseCharge() {
        Charging = false;

        bullet.Shoot(transform.forward);
    }

    private void LoadBullet() {
        var initialDistanceToBullet = startRadius + bulletOffset;
        bullet.SetPosition(transform.position + transform.forward * initialDistanceToBullet);
        bullet.SetRadiusToMinimum();
    }

}
