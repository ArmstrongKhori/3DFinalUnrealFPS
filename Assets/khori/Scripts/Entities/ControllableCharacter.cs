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
        input.Read();


        Vector3 moveDirection = Vector3.zero;
        //
        moveDirection += new Vector3(10 * input.strafing, 0, 10 * input.moving);
        //
        transform.Translate(moveDirection * Time.deltaTime);


        if (input.fire1)
        {
            BattleManager.Instance().Spawn("Bullet", this);
        }
    }

}
