using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents solid things in the game space.
/// It has the added benefit of carrying sound information and other parameters to generate realism.
/// </summary>
public class SolidSurface : Entity {

    public enum SurfaceTypes
    {
        /// <summary>
        /// Undescribable; Has no reaction.
        /// </summary>
        None,
        /// <summary>
        /// Leaves a crater when impacted.
        /// </summary>
        Stone,
        /// <summary>
        /// Leaves a splintered hole when impacted.
        /// </summary>
        Wood,
        /// <summary>
        /// Leaves a dent when impacted.
        /// </summary>
        Metal,
        /// <summary>
        /// Leaves a broken hole when impacted.
        /// </summary>
        Glass,
        /// <summary>
        /// Leaves a sharp hole when impacted.
        /// </summary>
        Plastic,
    }
    public SurfaceTypes surfaceType = SurfaceTypes.None;


    public enum ImpactMagnitudes
    {
        /// <summary>
        /// There was no impact.
        /// </summary>
        None,
        /// <summary>
        /// Descriptive of being struck by something small (like bullets or pellets.)
        /// </summary>
        Light,
        /// <summary>
        /// Descriptive of being hit with an object (like a rock or a hammer.)
        /// </summary>
        Moderate,
        /// <summary>
        /// Descriptive of being smash by something large (like a car or an explosion.)
        /// </summary>
        Heavy,
        /// <summary>
        /// The impact (hypothetically) caused the thing to be destroyed.
        /// </summary>
        Break,
    }


    /// <summary>
    /// Describes a situation where force is applied against the object.
    /// </summary>
    /// <param name="magnitude"></param>
    /// <param name="direction">The angle of the impact (relative to the center of the object.)</param>
    public void Impact(ImpactMagnitudes magnitude, Vector3 direction)
    {
        ImpactResult ir = new ImpactResult();
        //
        // ??? <--


        //
        OnImpacted(magnitude, direction, ir);
    }


    public virtual void OnImpacted(ImpactMagnitudes magnitude, Vector3 direction, ImpactResult ir)
    {

    }

}

/// <summary>
/// A descriptive of exactly what happened due to the impact.
/// </summary>
public class ImpactResult
{

}
