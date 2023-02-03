using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResizableSphere))]
public class Character : MonoBehaviour {

    [SerializeField] private ResizableSphere bulletSphere;
    [SerializeField] private float radiusUnitsPerSeconds = 1;
    
    private ResizableSphere _characterSphere;

    public bool Shrinking { get; private set; }

    private void Awake() {
        _characterSphere = GetComponent<ResizableSphere>();
    }

    public void ActivateShrinking() {
        Shrinking = true;
    }

    public void StopShrinking() {
        Shrinking = false;
    }

    private void Update() {
        Shrinking = Input.GetKey(KeyCode.Space);

        if (Shrinking) {
            var radiusDelta = radiusUnitsPerSeconds * Time.deltaTime;
            _characterSphere.ChangeRadius(-radiusDelta);
            bulletSphere.ChangeRadius(radiusDelta);
        }
    }

}
