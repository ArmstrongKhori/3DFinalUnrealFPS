using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "Weapon" are abstract descriptions of the "weaponry" a character possesses, as well as its state.
/// Essentially, this is what a weapon is when its being held by a character (as opposed to lying on the floor or sitting in a pick-up spot.)
/// </summary>
public class Weapon
{

    public int currentAmmo = 0;
    public float fireWaitTime = 0.0f;


    /// <summary>
    /// Upon creating a weapon, you "install" weapon data.
    /// It is "readonly" so that you can't change this after it's created... Trust me, you don't want to.
    /// </summary>
    public readonly WeaponData data;
    public readonly ControllableCharacter owner;
    public Weapon(WeaponData wd, ControllableCharacter c)
    {
        data = wd;
        owner = c;
        //
        //
        wd.OnInitialize(this);
        //
        currentAmmo = wd.initialAmmo;
        //
        wd.OnCreate(this);
    }



    /// <summary>
    /// This should be placed wherever a fixed update is done
    /// </summary>
    public void Check()
    {
        if (fireWaitTime > 0)
        {
            fireWaitTime -= Time.deltaTime;
            //
            if (fireWaitTime <= 0)
            {
                data.OnReady(this);
            }
        }
    }

    public void Interact()
    {
        switch (data.firingMode)
        {
            case WeaponData.FiringModes.None:
                // *** We don't interact with it.
                break;
            case WeaponData.FiringModes.Press:
                if (owner.input.fire1 && !owner.input.lastFire1)
                {
                    data.TryFire(this);
                }
                break;
            case WeaponData.FiringModes.Hold:
                if (owner.input.fire1)
                {
                    data.TryFire(this);
                }
                break;
            case WeaponData.FiringModes.Release:
                if (owner.input.lastFire1 && !owner.input.fire1)
                {
                    data.TryFire(this);
                }
                break;
        }
    }
}

