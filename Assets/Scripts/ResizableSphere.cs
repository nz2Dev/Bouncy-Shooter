using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizableSphere : MonoBehaviour {

    [SerializeField] private GameObject geometry;
    
    public void ChangeRadius(float radiusDelta) {
        var geometryScale = geometry.transform.localScale;
        geometryScale += radiusDelta * Vector3.one;
        geometry.transform.localScale = geometryScale;
    }

}
