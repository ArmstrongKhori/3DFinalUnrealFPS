﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ***
 * An "Inputter" is a class that "objectifies" input.
 * The idea here is that ANYTHING, whatsoever, can be controlled, and what manner it does so is decided by what type of "Inputter" it is given.
 * */
/// <summary>
/// The "BaseInputter" initializes all inputs (as though the controller were rested upon the ground and nobody were using it.)
/// All children of BaseInputter should call its "Read" function so that their inputs are initialized as well... Or maybe you don't want to?
/// </summary>
public class BaseInputter {

    public float moving = 0.0f;
    public float strafing = 0.0f;
    public bool jump = false;
    public bool fire1 = false;
    public bool fire2 = false;
    public bool debug1 = false;
    public bool debug2 = false;
    public bool debug3 = false;
    public bool debug4 = false;
    //
    // *** All of these values are necessary for knowing if/when a button has been pushed/released...
    public float lastMoving = 0.0f;
    public float lastStrafing = 0.0f;
    public bool lastJump = false;
    public bool lastFire1 = false;
    public bool lastFire2 = false;
    public bool lastDebug1 = false;
    public bool lastDebug2 = false;
    public bool lastDebug3 = false;
    public bool lastDebug4 = false;


    public virtual void Read()
    {
        lastMoving = moving;
        lastStrafing = strafing;
        lastJump = jump;
        lastFire1 = fire1;
        lastFire2 = fire2;
        lastDebug1 = debug1;
        lastDebug2 = debug2;
        lastDebug3 = debug3;
        lastDebug4 = debug4;
        //
        moving = 0.0f;
        strafing = 0.0f;
        jump = false;
        fire1 = false;
        fire2 = false;
        debug1 = false;
        debug2 = false;
        debug3 = false;
        debug4 = false;

    }
}
