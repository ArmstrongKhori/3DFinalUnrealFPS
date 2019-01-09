using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDashAbility : Ability
{

public WaveDashAbility(ControllableCharacter cc) : base(cc)
{

}

public override void Init()
{
    base.Init();
        //
        activationMode = ActivationMode.SinglePress;
}
public override void Run()
{
    base.Run();
        //
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("FUCK");
            RegisterKill();
        }
    }
public override void LateRun()
{
    base.LateRun();
    //

}
public override void RunWhileActive()
{
    base.RunWhileActive();
    //

}
public override void RunWhileInactive()
{
    base.RunWhileInactive();
    //

}
public override void OnKill()
{
    base.OnKill();
    //

}
public override void OnFiveKills()
{
    base.OnFiveKills();
    //

}
public override void OnTriggered()
{
    base.OnTriggered();
        //
    Debug.Log("HERE WE USE THIS ABILITY!!!");

}
public override void OnActivate()
{
    base.OnActivate();
    //
}
public override void OnDeactivate()
{
    base.OnDeactivate();
    //

}
}
