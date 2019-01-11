using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunAbility : Ability {


    /// <summary>
    /// Constructor.
    /// PLEASE call the "base()" part or else you'll break everything!
    /// </summary>
    /// <param name="cc"></param>
    public RailgunAbility(ControllableCharacter cc) : base(cc)
    {

    }



    public override void Init()
    {
        base.Init();
        //
        activationMode = ActivationMode.HoldAndRelease;
        activatedDuration = 10.0f; // *** You can hold it for 10 seconds.
    }
    public override void Run()
    {
        base.Run();
        //
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
        BattleManager.Instance().Spawn("RailgunShot", owner, owner.LookVector);
    }


}
