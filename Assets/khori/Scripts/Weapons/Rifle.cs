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
    }

    public override void OnFire(Weapon w)
    {
        base.OnFire(w);
        //
        w.owner.pch.CmdSpawn("Bullet", w.owner.NetworkID, w.owner.LookVector);
    }

}
