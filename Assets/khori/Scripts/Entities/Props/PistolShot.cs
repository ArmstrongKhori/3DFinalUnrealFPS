using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShot : RCProp {

    public override void OnSpawned()
    {
        base.OnSpawned();
        //
        heading = Owner.LookVector;
    }


    public override bool OnResolved(ResolutionResult rr)
    {
        foreach (StrikingData sd in rr.allStrikes)
        {
            // ??? <-- I should probably catch this somewhere...
            BulletTrail.Create(sd.originPoint, sd.pointOfImpact, 1.0f, Color.yellow);
        }
        //
        return base.OnResolved(rr);
    }

}
