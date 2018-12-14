using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describes every detail about how the resolution occurred.
/// This information is important for adding feedback to the results (such as knowing the exact course many casts went in.)
/// </summary>
public class ResolutionResult
{
    public readonly RCProp prop;
    public List<StrikingData> allStrikes = new List<StrikingData>();
    /// <summary>
    /// A number used for generating the deviance.
    /// Knowing this number allows one to recreate the results of a "random" raycast.
    /// </summary>
    public float randomRoll;


    public ResolutionResult(RCProp p)
    {
        prop = p;
    }

    public void AddStrikeData(StrikingData sd)
    {
        allStrikes.Add(sd);
    }
}
