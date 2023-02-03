using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySphere : MonoBehaviour {

    [SerializeField] private GameObject geometry;
    [SerializeField] private float radiusUnitsPerSeconds = 1;
    
    public bool Shrinking { get; private set; }

    public void ActivateShrinking() {
        Shrinking = true;
    }

    public void StopShrinking() {
        Shrinking = false;
    }

    private void Update() {
        Shrinking = Input.GetKey(KeyCode.Space);

        if (Shrinking) {
            var geometryScale = geometry.transform.localScale;
            geometryScale -= radiusUnitsPerSeconds * Time.deltaTime * Vector3.one;
            geometry.transform.localScale = geometryScale;
        }
    }

}
