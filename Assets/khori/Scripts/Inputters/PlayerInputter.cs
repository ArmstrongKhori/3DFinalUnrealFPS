using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A "PlayerInputter" receives input directly from the/a user.
/// These should be assigned to things that must receive direct control from the player.
/// </summary>
public class PlayerInputter : BaseInputter {
    
    public override void Read()
    {
        base.Read();
        //
        moving = Input.GetAxis("Vertical");
        strafing = Input.GetAxis("Horizontal");
        jump = Input.GetButton("Jump");
        fire1 = Input.GetButton("Fire1");
        fire2 = Input.GetButton("Fire2");
        debug1 = Input.GetKey(KeyCode.V);
        debug2 = Input.GetKey(KeyCode.B);
        debug3 = Input.GetKey(KeyCode.N);
        debug4 = Input.GetKey(KeyCode.M);
    }
}
