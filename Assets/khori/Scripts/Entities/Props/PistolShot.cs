using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShot : RCProp {

    public override void OnSpawned(Vector3 lookVector)
    {
        base.OnSpawned(lookVector);
        //
        heading = lookVector; // Helper.GetNetworkActor(Owner).LookVector
    }


    public override bool OnResolved(ResolutionResult rr)
    {
        AudioManager.Instance().GUNSHOT();

        foreach (StrikingData sd in rr.allStrikes)
        {
            // ??? <-- I should probably catch this somewhere...
            CharacterOwner.pch.CmdCreateBulletTrail(sd.originPoint, sd.pointOfImpact, 0.1f, Color.yellow);
        }
        //
        return base.OnResolved(rr);
    }
}
