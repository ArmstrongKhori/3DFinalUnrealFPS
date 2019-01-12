using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A camera-point that mimics the player's movements.
/// Allows a "player perspective" type of camera.
/// </summary>
public class PlayerCam : Cam {


    public Character c;


    public override void Record()
    {
        base.Record();
        //
        Debug.Log("Camera: " + c.name);
        if (c != null)
        {
            Camera.transform.position = transform.position;
            Camera.transform.rotation = Quaternion.LookRotation(c.LookVector);
        }
    }
}
