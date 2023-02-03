using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResizableSphere))]
public class Character : MonoBehaviour {

    [SerializeField] private ResizableSphere bulletSphere;
    [SerializeField] private float radiusUnitsPerSeconds = 1;
    
    private ResizableSphere _characterSphere;

    public bool Charging { get; private set; }

    private void Awake() {
        _characterSphere = GetComponent<ResizableSphere>();
    }

    private void Update() {
        Charging = Input.GetKey(KeyCode.Space);

        if (Charging) {
            var radiusDelta = radiusUnitsPerSeconds * Time.deltaTime;
            _characterSphere.ChangeRadius(-radiusDelta);
            bulletSphere.ChangeRadius(radiusDelta);
        }
    }

}
