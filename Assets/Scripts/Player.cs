using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class Player : MonoBehaviour {

    [SerializeField] private Ball bulletSphere;
    [SerializeField] private float radiusUnitsPerSeconds = 1;
    [SerializeField] private float startRadius = 5;
    [SerializeField] private float bulletOffset = 0.5f;
    
    private Ball _characterSphere;

    public bool Charging { get; private set; }

    private void Awake() {
        _characterSphere = GetComponent<Ball>();
    }

    private void Start() {
        _characterSphere.SetRadius(startRadius);
        bulletSphere.SetRadiusToMinimum();
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

        bulletSphere.SetRadiusToMinimum();
        var initialDistanceToBullet = _characterSphere.Radius + bulletSphere.Radius + bulletOffset;
        bulletSphere.transform.position = transform.position + transform.forward * initialDistanceToBullet;
    }

    private void Charge() {
        var radiusDelta = radiusUnitsPerSeconds * Time.deltaTime;
        _characterSphere.ChangeRadius(-radiusDelta);
        bulletSphere.ChangeRadius(radiusDelta);
    }

    private void ReleaseCharge() {
        Charging = false;

        _characterSphere.SetRadius(startRadius);
        bulletSphere.SetRadiusToMinimum();
    }

}
