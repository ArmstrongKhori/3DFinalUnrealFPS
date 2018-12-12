using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Prop {

    public float initialVelocity = 0.0f;



    public override void OnSpawned(Actor by = null)
    {
        base.OnSpawned();
        //
        rb.useGravity = false;
        rb.velocity = by.transform.forward * initialVelocity;
    }

}
