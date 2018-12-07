using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableCharacter : Character {

    public BaseInputter input;

    public override void Awake()
    {
        base.Awake();
        //
        input = new PlayerInputter();
    }


    public override void Act()
    {
        base.Act();
        //


        Vector3 moveDirection;
        //




        // transform.Translate();
    }

}
