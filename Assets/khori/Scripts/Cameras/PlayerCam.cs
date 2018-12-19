using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : Cam {


    public Character c;


    public override void Record()
    {
        base.Record();
        //
        if (c != null)
        {
            Camera.transform.position = transform.position;
            Camera.transform.rotation = Quaternion.LookRotation(c.LookVector);
        }
    }
}
