using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInputter {

    public float moving = 0.0f;
    public float strafing = 0.0f;
    public bool jump = false;
    public bool fire1 = false;
    public bool fire2 = false;

    
    public virtual void Read()
    {
        moving = 0.0f;
        strafing = 0.0f;
        jump = false;
        fire1 = false;
        fire2 = false;
    }
}
