using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : WeaponData
{

    public override void OnCreate(Weapon w)
    {
        base.OnCreate(w);
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
        // ??? <-- Debugging code.
        BattleManager.Instance().Spawn("Bullet", w.owner);
    }

}
