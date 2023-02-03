using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizableSphere : MonoBehaviour {

    [SerializeField] private GameObject geometry;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float minRadius = 0.1f;
    [SerializeField] private float maxRadius = 5;

    private void Start() {
        radius = Mathf.Clamp(radius, minRadius, maxRadius);
        SyncScaleWithRadius();
    }

    public void ChangeRadius(float radiusDelta) {
        radius += radiusDelta;
        radius = Mathf.Clamp(radius, minRadius, maxRadius);
        SyncScaleWithRadius();
    }

    private void SyncScaleWithRadius() {
        geometry.transform.localScale = Vector3.one * radius;
    }

}
