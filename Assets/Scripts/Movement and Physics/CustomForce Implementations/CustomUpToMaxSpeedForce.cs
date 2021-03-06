﻿using UnityEngine;
using System;

[Serializable]
public class CustomUpToMaxSpeedForce : ICustomForceImplementation
{

    [SerializeField]
    private Vector3 constantForce;
    [SerializeField]
    private float upToSpeed;

    public Vector3 GetCurrentForceVector(CustomForce parentForce, ForceObject objectAppliedTo)
    {
        return (Vector3.Project(constantForce, objectAppliedTo.GetRecentNetSpeed()).magnitude > upToSpeed)? Vector3.zero : constantForce;
    }


    public CustomUpToMaxSpeedForce(Vector3 _constantForce, float _upToSpeed)
    {
        constantForce = _constantForce;
        upToSpeed = _upToSpeed;
    }

}