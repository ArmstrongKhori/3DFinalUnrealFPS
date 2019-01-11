using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunShot : RCProp
{

    public override void OnSpawned(Vector3 lookVector)
    {
        base.OnSpawned(lookVector);
        //
        heading = lookVector; // Helper.GetNetworkActor(Owner).LookVector
        //
        power = 200;
        piercesWalls = true;
    }


    public override bool OnResolved(ResolutionResult rr)
    {
        AudioManager.Instance().GUNSHOT();

        foreach (StrikingData sd in rr.allStrikes)
        {
            // ??? <-- I should probably catch this somewhere...
            CharacterOwner.PCH.CmdCreateBulletTrail(sd.originPoint, sd.pointOfImpact, 0.1f, Color.yellow);
        }
        //
        return base.OnResolved(rr);
    }
}
