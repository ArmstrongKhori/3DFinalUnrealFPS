using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettedCam : Cam {

    public Character target;


    private void LateUpdate()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
    }

}
