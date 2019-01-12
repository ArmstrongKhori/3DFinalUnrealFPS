using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability
{

    /// <summary>
    /// Constructor.
    /// PLEASE call the "base()" part or else you'll break everything!
    /// </summary>
    /// <param name="cc"></param>
    public HealAbility(ControllableCharacter cc) : base(cc)
    {

    }

    public override void Init()
    {
        base.Init();

        activationMode = ActivationMode.Lingering;      
    }

    public override void Run()
    {
        base.Run();
        //

        if (Input.GetKey(KeyCode.Q))
        {
            RegisterKill();
        }

        Debug.Log("TESTING RAILGUN");


    }
    public override void LateRun()
    {
        base.LateRun();
        //
    }
    public override void RunWhileActive()
    {
        base.RunWhileActive();
        ErickStatus.AlterHealth(15 * Time.deltaTime);

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
        ErickStatus.AlterHealth(50);
        //

    }
    public override void OnDeactivate()
    {
        base.OnDeactivate();
        //
      
    }


}
