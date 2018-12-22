using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Weapon "data" is the information about the weapon itself-- Such as what it looks like, what it does when it is fired, etc...
/// </summary>
public class WeaponData
{
    /// <summary>
    /// How many units of ammo this weapon has.
    /// Use zero or a negative number to specify that it can hold an unlimited amount of ammo
    /// </summary>
    public int maximumAmmo = 100;
    /// <summary>
    /// How much ammo does the gun start with when you pick it up?
    /// </summary>
    public int initialAmmo = 50;
    /// <summary>
    /// How much ammo it consumes when fired.
    /// Use zero or a negative number to specify that it has unlimited ammo.
    /// </summary>
    public int ammoCost = 1;
    /// <summary>
    /// How long (in seconds) you must wait before you can fire again.
    /// </summary>
    public float fireRate = 1.0f;

    public enum FiringModes
    {
        /// <summary>
        /// Cannot be fired. (holding a flag? who knows...)
        /// </summary>
        None,
        /// <summary>
        /// Only fires when "Fire" button is pressed.
        /// </summary>
        Press,
        /// <summary>
        /// Continues to fire as long as "Fire" button is pressed.
        /// </summary>
        Hold,
        /// <summary>
        /// Only fires when "Fire" button is released.
        /// </summary>
        Release,
    }
    /// <summary>
    /// How does the weapon interact with the "Fire" button?
    /// </summary>
    public FiringModes firingMode = FiringModes.None;

    // -----------------------------------------------------------------------------------
    // ??? <-- These are some dummy "TODO" parameters...
    /// <summary>
    /// The "model" that the gun uses and animates as.
    /// </summary>
    public string appearance = "nothing";
    /// <summary>
    /// The amount your view kicks up when shooting.
    /// "0" represents no recoil, while "1" is whiplashing your neck 90 degrees up.
    /// </summary>
    public float recoilStrength = 0.0f;

    /// <summary>
    /// How much the weapon's aim deviates when fires with minimum recoil reeling
    /// </summary>
    public float devianceMinimum = 0.0f;
    /// <summary>
    /// How much the weapon deviates when fired at maximum recoil reeling (IE: sustained fire with a machine gun.)
    /// </summary>
    public float devianceMaximum = 1.0f;
    // -----------------------------------------------------------------------------------




    public virtual bool IsReady(Weapon w)
    {
        return (w.fireWaitTime <= 0);
    }


    /// <summary>
    /// Unlike "Fire", this performs checks to see if firing is actually possible.
    /// It also correctly depletes ammo or performs the "no ammo" function if there is none left.
    /// </summary>
    /// <param name="w"></param>
    public virtual void TryFire(Weapon w) {

        if (!IsReady(w)) {
            Debug.Log("WeaponData: Not ready");
            return;
        }


        if (w.currentAmmo < ammoCost)
        {
            Debug.Log("WeaponData: No ammo");
            OnInsufficientAmmo(w);
            //
            // !!! <-- Should we "nudge" the character into switching guns?
        }
        else
        {
            Debug.Log("WeaponData: Firing!");
            Fire(w);
        }
    }

    public void Fire(Weapon w)
    {
        w.currentAmmo -= ammoCost; // *** Deplete ammo...
        w.fireWaitTime += fireRate; // *** Delay next shot...
        //
        OnFire(w);
        //
        // *** Apply recoil AFTER it is fired!
        w.owner.ApplyRecoil(recoilStrength);
    }




    public virtual void OnInitialize(Weapon w)
    {
        // *** Information about the weapon! Max ammo? How it fires? All here!
    }

    public virtual void OnFire(Weapon w)
    {
        // *** Spawn the bullets!?
    }

    public virtual void OnCreate(Weapon w)
    {
        // *** Set up the weapon's object pool here or load models/textures, etc...
    }

    public virtual void OnReady(Weapon w)
    {
        // *** A satisfying click? Maybe a whirring for laser guns?
    }

    public virtual void OnInsufficientAmmo(Weapon w)
    {
        // *** Click... Click...
    }

}
