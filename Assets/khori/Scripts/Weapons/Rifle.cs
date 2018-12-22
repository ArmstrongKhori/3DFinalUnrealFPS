using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : WeaponData
{

    public override void OnInitialize(Weapon w)
    {
        base.OnInitialize(w);
        //
        initialAmmo = 60;
        maximumAmmo = 300;
        fireRate = 1 / 32.0f;
        firingMode = FiringModes.Hold;
        appearance = "rifle";
        recoilStrength = 0.07f;
        devianceMinimum = 0.02f;
        devianceMaximum = 0.12f;
    }

    public override void OnFire(Weapon w)
    {
        base.OnFire(w);
        //
        Vector3 lookVec = Helper.DevianceAdjustedLook(w.owner.LookVector, Mathf.Lerp(devianceMinimum, devianceMaximum, w.owner.RecoilValue));
        //
        Debug.Log("trying to spawn a thing...");
        w.owner.pch.CmdSpawn("Bullet", w.owner.NetworkID, lookVec);
    }

}
