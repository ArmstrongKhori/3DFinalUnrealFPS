using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "Props" are things created or manipulated by actors (such as bullets, flashes of light, explosions, rubble, etc...)
/// Contrary to its name, it DOES NOT represent things that are anchored and/or attached to a character.
/// </summary>
public class Prop : Actor {

    /// <summary>
    /// The amount of damage this prop causes upon striking a character.
    /// </summary>
    public float power = 10;
    /// <summary>
    /// The furthest area this prop covers.
    /// Mainly used for when it is being resolved or if it has a diminishing area of effect.
    /// </summary>
    public float radiusMax = 1.0f;
    /// <summary>
    /// The closest point at which the prop begins to diminish.
    /// As long as a character is within this distance, there will be no diminishing whatsoever.
    /// </summary>
    public float radiusMin = 0.1f;
    /// <summary>
    /// Does this prop have diminishing effect the further a character is from where it resolved?
    /// </summary>
    public bool hasDiminishingArea = true;


    public override void Act()
    {
        base.Act();
        //

    }


    public virtual void Strike(StrikingData data)
    {
        if (data.character != null)
        {
            data.character.Struck(this, data);
        }

        if (data.surface != null)
        {
            // ??? <-- Hard-coded impact value...
            data.surface.Impact(SolidSurface.ImpactMagnitudes.Light, (data.pointOfImpact - data.surface.transform.position));
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        StrikingData sd = new StrikingData(this);
        //
        //
        // *** Striking a character takes priority, even if they (somehow) double as a solid surface...
        Character c = collision.gameObject.GetComponent<Character>();
        if (c != null)
        {
            sd.character = c;
            sd.originPoint = transform.position;
            sd.pointOfImpact = collision.contacts[0].point;
        }
        else
        {
            SolidSurface ss = collision.gameObject.GetComponent<SolidSurface>();
            if (ss != null)
            {
                sd.surface = ss;
                sd.originPoint = transform.position;
                sd.pointOfImpact = collision.contacts[0].point;
            }
        }
        // ??? <-- Currently only takes ONE point of impact.
        // ??? <-- Always assumes we're using the 0th point of impact...
        //
        //
        Strike(sd);
    }
}
