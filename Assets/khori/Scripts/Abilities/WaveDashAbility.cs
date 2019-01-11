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
        activationMode = ActivationMode.Lingering;
        activatedDuration = 1.5f;
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
        if(KhoriController.input.jump && !KhoriController.input.lastJump)
        {
            owner.transform.Translate(new Vector3(RickController.HPress * 5, 0, RickController.VPress * 5));
        }
        RickController.ActivateTrail();
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

        RickController.NoJump = true;
}
    public override void OnDeactivate()
    {
        base.OnDeactivate();
        //
        RickController.NoJump = false;

        RickController.DeactivateTrail();
    }
}
