using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCProp : Prop {

    internal Vector3 startingPoint;

    public enum ResolutionShapes
    {
        /// <summary>
        /// No shape; does not hit anything.
        /// </summary>
        None,
        /// <summary>
        /// Produces raycasts that deviate from the targetted spot (up to [degrees] degrees.)
        /// Always strikes the closest thing to its starting point.
        /// </summary>
        Conal,
        /// <summary>
        /// Checks for line of sight and whether potential targets are within a certain radius.
        /// </summary>
        Sphere, 
    }
    public ResolutionShapes resolutionShape = ResolutionShapes.None;


    /// <summary>
    /// The "direction" on which this prop intends to resolve.
    /// This is mainly only relevant for bullets, which describes the direction it will go.
    /// </summary>
    internal Vector3 heading = Vector3.zero;
    /// <summary>
    /// How many degrees of deviance can occur when resolving.
    /// </summary>
    public float degrees = 10.0f;
    /// <summary>
    /// How far this raycast can travel before fizzling.
    /// </summary>
    public float falloff = 1000.0f;
    /// <summary>
    /// How many times does this resolve?
    /// This is mainly relevant for "bursty" things, such as shotguns or shrapnel... Or maybe an absurdly fast minigun? *thinks of Roadhog...*
    /// </summary>
    public float count = 1;


    public override void Awake()
    {
        base.Awake();
        //
        rb.useGravity = false;
        // ??? <-- Raycasted Props shouldn't even HAVE rigidbodies...
    }

    public override void OnSpawned(Actor by = null)
    {
        base.OnSpawned(by);
        //
        startingPoint = transform.position;
    }


    public override void Act()
    {
        base.Act();
        //
        // ??? <-- This is probably not technically the "best" way to resolve the prop...
        Resolve();
    }



    /// <summary>
    /// Performs the ray-casting check, applying any effects it has, then destroying/detonating the prop.
    /// </summary>
    public void Resolve()
    {
        Debug.Log("Resolving as: " + resolutionShape);

        ResolutionResult rr = new ResolutionResult(this);
        rr.randomRoll = Random.Range(0f, 100f);
        //
        RaycastHit[] rays;
        switch (resolutionShape)
        {
            case ResolutionShapes.None:
                break;
            case ResolutionShapes.Conal:
                // ??? <-- This needs CORRECT layer masks...
                // ??? <-- There is currently no deviance for raycasting whatsoever...
                // !!! <-- Use the 10-modded value of the roll for left-right, and the 10-divved value of the roll for the up-down...
                rays = Physics.RaycastAll(startingPoint, heading, falloff, 1 << 9| 1 << 10);
                Debug.Log("Heading: " + heading);
                //
                // *** Let's find the closest point of impact out of all possible candidates!
                int closest = -1;
                for (int i = 0; i < rays.Length; i++)
                {
                    RaycastHit ray = rays[i];

                    // *** It MUST be hitting an "entity", or else it doesn't count.
                    if (ray.collider.gameObject.GetComponent<Entity>() == null)
                    {
                        continue;
                    }


                    if (closest < 0)
                    {
                        closest = i;
                    }
                    else if (ray.distance < rays[closest].distance)
                    {
                        closest = i;
                    }
                }
                //
                // *** We hit something! So...
                if (closest >= 0)
                {
                    // *** Move the prop to the closest point of impact...
                    transform.position = rays[closest].point;
                    //
                    // *** ... Then, STRIKE that thing!
                    StrikingData sd = new StrikingData(this);
                    sd.originPoint = startingPoint;
                    sd.pointOfImpact = transform.position;
                    sd.character = rays[closest].collider.gameObject.GetComponent<Character>();
                    sd.surface = rays[closest].collider.gameObject.GetComponent<SolidSurface>();
                    //
                    rr.AddStrikeData(sd);
                }
                else
                {
                    // *** Go to the furthest point it would have gone.
                    transform.position = startingPoint + heading * falloff;
                    //
                    // *** Create striking data anyways, but it didn't "hit" anything.
                    StrikingData sd = new StrikingData(this);
                    sd.originPoint = startingPoint;
                    sd.pointOfImpact = transform.position;
                    sd.character = null;
                    sd.surface = null;
                    //
                    rr.AddStrikeData(sd);
                }
                break;
            case ResolutionShapes.Sphere:
                // ??? <-- This needs CORRECT layer masks...
                rays = Physics.SphereCastAll(startingPoint, radiusMax, Vector3.zero, 1 << 9 | 1 << 10);
                //
                foreach (RaycastHit ray in rays)
                {
                    // *** It MUST be hitting an "entity", or else it doesn't count.
                    if (ray.collider.gameObject.GetComponent<Entity>() == null)
                    {
                        continue;
                    }


                    // *** ... Then, STRIKE that thing!
                    StrikingData sd = new StrikingData(this);
                    sd.originPoint = startingPoint;
                    sd.pointOfImpact = ray.point;
                    sd.character = ray.collider.gameObject.GetComponent<Character>();
                    sd.surface = ray.collider.gameObject.GetComponent<SolidSurface>();
                    //
                    rr.AddStrikeData(sd);
                }
                break;
        }
        //
        //
        if (OnResolved(rr))
        {
            foreach (StrikingData sd in rr.allStrikes)
            {
                Strike(sd);
            }
        }
        //
        //
        Despawn();
    }


    /// <summary>
    /// What should happen when this prop resolves?
    /// </summary>
    /// <param name="rr"></param>
    /// <returns>Should we perform all the strikes?</returns>
    public virtual bool OnResolved(ResolutionResult rr)
    {
        return true;
    }
}
