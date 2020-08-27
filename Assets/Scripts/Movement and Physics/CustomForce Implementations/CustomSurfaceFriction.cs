﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSurfaceFriction : ICustomForceImplementation
{


    public Vector3 NormalVector { get; set; }

    public float FrictionForceMultiplier { get; set; }

    private ForceObject currentParent;

    private CustomForce currentForceInstance;


    public CustomSurfaceFriction(Vector3 normalizedNormalVector, float frictionForceMultiplier)
    {
        NormalVector = normalizedNormalVector;
        FrictionForceMultiplier = frictionForceMultiplier;
    }

    //TODO will caching getter values change performance?

    public Vector3 GetCurrentForceVector(CustomForce parentForce, ForceObject objectAppliedTo)
    {
        Vector3 projection = Vector3.Project(objectAppliedTo.GetRecentNetAcceleration(), NormalVector);
        if (projection.magnitude < (projection - NormalVector).magnitude)
            return Vector3.zero;
        return (-projection * FrictionForceMultiplier);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parentObject"></param>
    /// <param name="isPure"></param>
    /// <param name="applyFor"> set to negative infinity for application until not. </param>
    /// <returns></returns>
    public void ApplyForce(ForceObject parentObject, bool isPure, float applyFor)
    {
        currentParent = parentObject;
        currentForceInstance = new CustomForce(parentObject, this, isPure, applyFor, true);
    }

    public bool CeaseForceApplication()
    {
        if (currentParent == null)
            return false;

        currentParent.RemoveForce(currentForceInstance);
        currentParent = null;
        currentForceInstance = null;
        return true;
    }

}
