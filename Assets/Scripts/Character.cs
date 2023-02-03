using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResizableSphere))]
public class Character : MonoBehaviour {

    [SerializeField] private ResizableSphere bulletSphere;
    [SerializeField] private float radiusUnitsPerSeconds = 1;
    [SerializeField] private float startRadius = 5;
    [SerializeField] private float bulletOffset = 0.5f;
    
    private ResizableSphere _characterSphere;

    public bool Charging { get; private set; }

    private void Awake() {
        _characterSphere = GetComponent<ResizableSphere>();
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
