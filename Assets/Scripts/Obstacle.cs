using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public event Action OnDetonate;

    [SerializeField] private float destroyDelay = 1f;

    public void DestroySelf() {
        OnDetonate?.Invoke();
        Destroy(gameObject, destroyDelay);
    }
}
