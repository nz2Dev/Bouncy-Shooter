using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVisuals : MonoBehaviour {
    
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform shockwaveGeometry;

    private void Awake() {
        bullet.OnRadiusChanged += BulletOnRadiusChanged;
    }

    private void BulletOnRadiusChanged() {
        shockwaveGeometry.localScale = Vector3.one * (bullet.ShockwaveRadius * 2f);
    }
    
}
