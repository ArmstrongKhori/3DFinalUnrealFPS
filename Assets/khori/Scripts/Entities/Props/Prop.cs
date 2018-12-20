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


    public ControllableCharacter CharacterOwner { get { return Helper.GetNetworkActor(Owner) as ControllableCharacter; } }


    public override void Act()
    {
        base.Act();
        //

    }


    public virtual void Strike(StrikingData data)
    {
        bool didStrike = false;
        //
        if (data.character != null)
        {
            didStrike = true;

            data.character.Struck(this, data);
        }

        if (data.surface != null)
        {
            didStrike = true;

            // ??? <-- Hard-coded impact value...
            data.surface.Impact(SolidSurface.ImpactMagnitudes.Light, (data.pointOfImpact - data.surface.transform.position));
        }


        if (didStrike)
        {
            Despawn();
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        //
        // Helper.DisplayMessage("I am " + name + ", my owner is " + Owner);
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        //
        // Helper.DisplayMessage("I am " + name + ", my owner is " + Owner);
    }


    private void OnTriggerEnter(Collider collision)
    {
        StrikingData sd;
        Actor me = Helper.GetNetworkActor(Owner);


        Character c = collision.gameObject.GetComponent<Character>();
        if (c != null)
        {
            // *** Don't check for a hit UNLESS you are the one who controls this character! (honour system)
            if (!c.isLocalPlayer) { }
            else
            {
                if (c.gameObject != me.gameObject)
                {
                    Debug.Log(me.name + " is hitting " + c.name);

                    sd = new StrikingData(this);
                    sd.character = c;
                    sd.originPoint = transform.position;
                    sd.pointOfImpact = collision.ClosestPoint(transform.position); // contacts[0].point
                                                                                   //
                    Strike(sd);
                }
            }
        }
        else
        {
            if (!me.isServer) { }
            else
            {
                SolidSurface ss = collision.gameObject.GetComponent<SolidSurface>();

                // *** Don't check for a hit UNLESS you are the server!
                if (ss != null)
                {
                    sd = new StrikingData(this);
                    sd.surface = ss;
                    sd.originPoint = transform.position;
                    sd.pointOfImpact = collision.ClosestPoint(transform.position); // collision.contacts[0].point;
                                                                                    //
                    Strike(sd);
                }
            }
        }


        // ??? <-- Currently only takes ONE point of impact.
        // ??? <-- Always assumes we're using the 0th point of impact...
        //
        //
        
    }
}
