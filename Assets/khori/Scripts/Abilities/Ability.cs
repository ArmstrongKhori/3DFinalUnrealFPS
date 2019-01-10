using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Ability { 


    /// <summary>
    /// Gets the controllable character written by Khori.
    /// This is where the gun-shooting logic is.
    /// </summary>
    public ControllableCharacter KhoriController { get { return owner; } }
    /// <summary>
    /// Gets the controllable character written by Rick.
    /// This is where the physics and movement logic is.
    /// </summary>
    public FPSController2 RickController { get { return owner.controller; } }
    /// <summary>
    /// Gets the animator written by Andre.
    /// This is where you manipulate the character's animations and whatnot.
    /// </summary>
    public CharacterStateManager AndreAnimator { get { return owner.stateManager; } }
    /// <summary>
    /// Gets the health/armor manager written by Erick.
    /// This is where you manipulate the character's health/armor... Or make them die.
    /// </summary>
    public PlayerHealth2 ErickStatus { get { return owner.healthStatus; } }



    internal ControllableCharacter owner;
    public Ability(ControllableCharacter cc)
    {
        owner = cc;
        //
        //
        killScore = 0;
        requiredKills = 5;
        usedCount = 0;
        isReady = false;
        isActivated = false;
        //
        //
        Init();
    }


    /// <summary>
    /// The scoring for how many kills you have.
    /// </summary>
    internal int killScore = 0;
    /// <summary>
    /// The "required" amount of kills before the ability activates.
    /// Change it if you like, but that would be breaking the rules!
    /// </summary>
    public int requiredKills = 5;
    /// <summary>
    /// Keeps track of the number of times you've used the ability.
    /// This is useful for if your ability has a "scaling" mechanic for every time you've used it.
    /// </summary>
    public int usedCount = 0;


    private bool isReady;
    public bool IsReady { get { return isReady; } }

    /// <summary>
    /// Keeps track of how long the ability should last.
    /// </summary>
    private float activeTimer;
    /// <summary>
    /// Is the ability on? (not TRIGGERED, activated!)
    /// </summary>
    internal bool isActivated;
    internal bool IsActivated { get { return isActivated; } }

    public void Interact(BaseInputter input)
    {
        if (IsActivated && activeTimer > 0)
        {
            activeTimer -= Time.deltaTime;
            //
            if (activeTimer <= 0)
            {
                Deactivate();
            }
        }



        if (input.fire2 && !input.lastFire2)
        {
            Trigger();
        }
    }


    public void Trigger()
    {
        usedCount += 1;
        //
        switch (activationMode)
        {
            case ActivationMode.None:
                break;
            case ActivationMode.SinglePress:
                OnTriggered();
                break;
            case ActivationMode.Lingering:
                Activate();
                break;
        }
        //
        //
        isReady = false;
    }

    public void Activate()
    {
        isActivated = true;
        activeTimer = activatedDuration;
        //
        OnActivate();
    }

    public void Deactivate()
    {

        isActivated = false;
        //
        OnDeactivate();
    }



    /// <summary>
    /// What happens the moment you get a kill.
    /// </summary>
    public void RegisterKill()
    {
        if (isReady) { return; }
        else if (isActivated) { return; }



        killScore += 1;
        //
        OnKill();
        //
        if (killScore >= requiredKills)
        {
            isReady = true;
            //
            OnFiveKills();
            //
            killScore = 0;
        }
    }


    public enum ActivationMode
    {
        /// <summary>
        /// Doesn't activate-- Ever.
        /// </summary>
        None,
        /// <summary>
        /// Does something the moment you activate it, then goes back to being inactive.
        /// </summary>
        SinglePress,
        /// <summary>
        /// Does something for a period of time, then goes back to being inactive.
        /// </summary>
        Lingering,
    }
    public ActivationMode activationMode;

    /// <summary>
    /// For "lingering" modes, how long does this remain active?
    /// </summary>
    public float activatedDuration;


    /// <summary>
    /// Any settings you might want to set the moment ability is plugged into the character.
    /// </summary>
    public virtual void Init()
    {
        activationMode = ActivationMode.None;
        activatedDuration = 3.0f; // *** 3 seconds.
    }

    /// <summary>
    /// A function that runs at every fixed update.
    /// You SHOULDN'T have logic here... Buuut... You can, if you want.
    /// </summary>
    public virtual void Run()
    {

    }
    /// <summary>
    /// A version of Run that happens AFTER "X-WhileActive".
    /// </summary>
    public virtual void LateRun()
    {

    }

    /// <summary>
    /// Every fixed update while the effect is active.
    /// </summary>
    public virtual void RunWhileActive()
    {

    }
    /// <summary>
    /// Every fixed update while the effect is INactive.
    /// </summary>
    public virtual void RunWhileInactive()
    {

    }




    /// <summary>
    /// Does anything happen for each kill you get?
    /// *global plinking noise*
    /// Khori: "I'm charging up!"
    /// </summary>
    public virtual void OnKill()
    {

    }


    /// <summary>
    /// What happens when your trigger is ready?
    /// *big light flash, global materializing noise*
    /// Announce: "X-Railgun is ready."
    /// </summary>
    public virtual void OnFiveKills()
    {

    }


    /// <summary>
    /// For "SinglePress" activation, this is what "happens" when you use the ability.
    /// Khori: "X-Railgun is set! You can't hide from me!"
    /// *switches my current gun with the railgun*
    /// </summary>
    public virtual void OnTriggered()
    {

    }


    /// <summary>
    /// For "lingering" activation, this is what happens right before the timer is started.
    /// </summary>
    public virtual void OnActivate()
    {

    }
    /// <summary>
    /// For "lingering" activation, this is what happens when time runs out.
    /// </summary>
    public virtual void OnDeactivate()
    {

    }


}



/* *** ABILITIES
 * Directional Wave-Dash ~ Temporarily enable an additional jump that can go in any desired direction.
 * Grappling Hook ~ Your next shot will pull you to the target and temporarily bind you there. You are invisible during this.
 * Team/Self Heal ~ Reverses all damage received (such that damage becomes healing). Affects you and nearby teammates.
 * X-Railgun ~ Gain a single powerful shot that has perfect accuracy, pierces walls, and allows you to see through walls.
 * Damage Reflector ~ Returns 200% of any damage received to the attacker.
 * */

/* GENERAL RULES:
    * You may TRIGGER the ability at any point after getting 5 kills.
    * You cannot "stockpile" triggers, meaning any kills after the 5th with an unused trigger are wasted.
    * You do not lose the trigger if you die.
    * Your kill total does not reset until you acquire the trigger.
    * */
