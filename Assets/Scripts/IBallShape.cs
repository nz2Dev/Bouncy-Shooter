using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBallShape {
    public event Action OnRadiusChanged;
    
    public float Radius { get; } 
}
