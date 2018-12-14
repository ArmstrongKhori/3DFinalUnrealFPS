using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponData {

    public override void OnCreate(Weapon w)
    {
        base.OnCreate(w);
        //
        initialAmmo = 12;
        maximumAmmo = 48;
        fireRate = 1 / 2.5f;
        firingMode = FiringModes.Press;
        appearance = "pistol";
    }

    public override void OnFire(Weapon w)
    {
        base.OnFire(w);
        //
        // ??? <-- Debugging code.
        // BattleManager.Instance().Spawn("Bullet", w.owner);
        BattleManager.Instance().Spawn("PistolShot", w.owner);
    }

}
