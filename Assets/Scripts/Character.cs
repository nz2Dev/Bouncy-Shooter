using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResizableSphere))]
public class Character : MonoBehaviour {

    [SerializeField] private float radiusUnitsPerSeconds = 1;
    
    private ResizableSphere _resizableSphere;

    public bool Shrinking { get; private set; }

    private void Awake() {
        _resizableSphere = GetComponent<ResizableSphere>();
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
            _resizableSphere.ChangeRadius(-radiusUnitsPerSeconds * Time.deltaTime);
        }
    }

}
