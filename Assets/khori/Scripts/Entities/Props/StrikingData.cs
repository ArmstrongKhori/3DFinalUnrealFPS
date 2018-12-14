using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikingData
{
    public readonly Prop prop;

    public Character character = null;
    public SolidSurface surface = null;

    public Vector3 originPoint;
    public Vector3 pointOfImpact;

    public StrikingData(Prop p)
    {
        prop = p;
    }
}
