using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Prop {

    public float initialVelocity = 0.0f;



    public override void OnSpawned()
    {
        base.OnSpawned();
        //
        rb.useGravity = false;
        rb.velocity = Owner.LookVector * initialVelocity;
    }

}
