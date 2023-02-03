using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {
    
    [SerializeField] private float speedUnitsPerSecond = 4f;

    private Rigidbody _rigidbody;
    private Vector3 _flyDirection;

    public bool Flying { get; private set; }

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.isKinematic = true;
    }

    public void Shoot(Vector3 direction) {
        _flyDirection = direction;
        Flying = true;
    }

    private void FixedUpdate() {
        if (Flying) {
            _rigidbody.MovePosition(Time.fixedDeltaTime * speedUnitsPerSecond * _flyDirection);
        }
    }

}
