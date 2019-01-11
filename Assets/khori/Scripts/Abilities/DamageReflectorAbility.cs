using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReflectorAbility : Ability {

    
    public DamageReflectorAbility(ControllableCharacter cc) : base(cc)
    {

    }

    public override void Init()
    {
        base.Init();
        //
        activationMode = ActivationMode.Lingering;
        activatedDuration = 30.0f;
        
    }
    public override void Run()
    {
        base.Run();
        //
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("ABILITY IS READY FOR USE");
            
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
    public override void OnActivate()
    {
        base.OnActivate();
        //

        Debug.Log("HERE ABILITY ACTIVATES");
        AndreAnimator.TurnIndicatorON();
        
        
    }
    public override void OnDeactivate()
    {
        base.OnDeactivate();
        //

         Debug.Log("DEACTIVATE ABILITY");
        AndreAnimator.TurnIndicatorOFF();
    }


    public override void OnTakenDamage(Character c, float val)
    {
        base.OnTakenDamage(c, val);
        //
        if (IsActivated)
        {

            ErickStatus.AlterHealth(20);
            Debug.Log("I hit you back!");
            // owner.Network_Interact(Character.InteractVerbs.Damage, c.NetworkID, owner.NetworkID, new InteractData(val));
        }
    }
}
