﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// A "Character" is an actor which interacts with projectiles, physics, or characters.
/// </summary>
public class Character : Actor
{
    /// <summary>
    /// Soft cap for health.
    /// Health above this amount will deplete by 1 every second.
    /// You also cannot heal above this amount with "ordinary" collectibles or regeneration.
    /// </summary>
    public const float HEALTHMAXSOFT = 100;
    /// <summary>
    /// Hard cap for health.
    /// No force, whatsoever, can put you above this total.
    /// </summary>
    public const float HEALTHMAXHARD = 200;

    /// <summary>
    /// Soft cap for armor.
    /// Only "special" armor collectibles can bring you above this amount.
    /// </summary>
    public const float ARMORMAXSOFT = 100;
    /// <summary>
    /// Hard cap for armor.
    /// No force, whatsoever, can put you above this total.
    /// </summary>
    public const float ARMORMAXHARD = 200;

    
    [SyncVar]
    public float armor = 25;
    [SyncVar]
    public float health = 100;



    public override Vector3 LookVector { get { return base.LookVector; } }



    public override void Awake()
    {
        base.Awake();
        //
        // *** "Characters" do not tilt. They can only "turn".
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }



    public void Struck(Prop prop, StrikingData data)
    {
        RpcNetworkGetHit();
    }

    
    [ClientRpc]
    public void RpcNetworkGetHit()
    {
        rb.velocity = new Vector3(0, 10, 0);
    }
}
