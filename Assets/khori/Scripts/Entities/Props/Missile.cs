using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Prop {

    public float initialVelocity = 0.0f;



    public override void OnSpawned(Vector3 lookVector)
    {
        base.OnSpawned(lookVector);
        //
        rb.useGravity = false;
        rb.velocity = lookVector * initialVelocity; // Helper.GetNetworkActor(Owner).LookVector


        AudioManager.Instance().AK47SHOT();
    }

}
