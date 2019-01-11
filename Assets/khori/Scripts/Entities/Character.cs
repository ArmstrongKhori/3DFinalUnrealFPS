using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// A "Character" is an actor which interacts with projectiles, physics, or characters.
/// It has special rules, such as walking, gravity, etc...
/// </summary>
public class Character : Actor
{
    /*
    /// <summary>
    /// Soft cap for health.
    /// Health above this amount will deplete by 1 every second.
    /// You also cannot heal above this amount with "ordinary" collectibles or regeneration.
    /// </summary>
    public const float HEALTHMAXSOFT = 100;
    /// <summary>
    /// Hard cap for health.
    /// No force, whatsoever, can put you above this total.
    /// </summary>
    public const float HEALTHMAXHARD = 200;

    /// <summary>
    /// Soft cap for armor.
    /// Only "special" armor collectibles can bring you above this amount.
    /// </summary>
    public const float ARMORMAXSOFT = 100;
    /// <summary>
    /// Hard cap for armor.
    /// No force, whatsoever, can put you above this total.
    /// </summary>
    public const float ARMORMAXHARD = 200;

    
    [SyncVar]
    public float armor = 25;
    [SyncVar]
    public float health = 100;
    */



    public PlayerHealth2 healthStatus;
    public GameObject modelHolder;
    public PlayerModel playerModel;
    public CharacterStateManager stateManager;

    public POI gunPoint;


    public NetworkInstanceId NetworkID { get { return GetComponent<NetworkIdentity>().netId; } }
    public override Vector3 LookVector { get { return base.LookVector; } }



    public override void Awake()
    {
        base.Awake();
        //
        // *** "Characters" do not tilt. They can only "turn".
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;


        modelHolder = transform.Find("Player").gameObject;
        //
        stateManager = modelHolder.GetComponent<CharacterStateManager>();

        healthStatus = GetComponent<PlayerHealth2>();
        healthStatus.currentHealthText = GameObject.Find("(erick)").transform.Find("Canvas").Find("CurrentHealthText").GetComponent<UnityEngine.UI.Text>();
        healthStatus.deadScreen = GameObject.Find("(erick)").transform.Find("Canvas").Find("deadScreen").GetComponent<UnityEngine.UI.Image>();

        name = "PlayerNo_" + FindObjectsOfType<Character>().Length;


    }
    public override void Start()
    {
        base.Start();
        //

        if (isLocalPlayer)
        {
            GameManager.Instance().DisplayMessage("You are " + name);
        }
        ///MIKE
      //  GameObject.Find("Networker").GetComponent<Networker>().SpawnedPlayer = this.gameObject;

        InitializeMe();
    }


    public virtual void InitializeMe()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }





    /// <summary>
    /// The character's "response" to being hit by a prop-- Usually as damage and grunting.
    /// </summary>
    /// <param name="prop"></param>
    /// <param name="data"></param>
    public void Struck(Prop prop, StrikingData data)
    {
        // *** The owner of the prop is responsible for dealing the damage. If there's no owner, you (the target) are considered as its owner.
        Character c = Helper.GetNetworkActor(prop.Owner) as Character;
        if (c == null) { c = this; }
        //
        GameManager.Instance().DisplayMessage(c.name + " strikes " + name);
        //
        Network_Interact(InteractVerbs.Damage, c.NetworkID, NetworkID, new InteractData(prop.power));
    }


    

    public virtual void Die()
    {
        stateManager.Death();
    }


    public virtual void Respawn()
    {
        stateManager.InitializeMe();
        healthStatus.InitializeMe();
        InitializeMe();
        //
        foreach (NetworkStartPosition pos in FindObjectsOfType<NetworkStartPosition>())
        {
            transform.position = pos.transform.position; // new Vector3(0, 0, 0)
            break;
        }
        //
        OnSpawned(LookVector);
    }




    /// <summary>
    /// These are verbs used when one client wants to interact with another client.
    /// </summary>
    public enum InteractVerbs
    {
        /// <summary>
        /// Does nothing.
        /// </summary>
        None,
        /// <summary>
        /// Displays a message (addressed from you)
        /// </summary>
        Tell,
        /// <summary>
        /// Cause damage.
        /// </summary>
        Damage,
    }

    /// <summary>
    /// Tells other players what you intend to do to them, as well as passing some data regarding "how" you're doing it.
    /// </summary>
    /// <param name="verb"></param>
    /// <param name="id"></param>
    /// <param name="data"></param>
    public void Network_Interact(InteractVerbs verb, NetworkInstanceId from, NetworkInstanceId to, InteractData data)
    {
        GameManager.Instance().AppendMessage("Within interact!");

        // ??? <-- This PROBABLY is sorta lazy, but... Eh.
        // *** I'm making sure to ALWAYS call this from the local player's context, even if they're not even involved in the interaction.
        ControllableCharacter localChar = Helper.GetLocalPlayer();
        localChar.pch.CmdNetwork_Interact(verb, from, to, data);
    }

    /// <summary>
    /// The player's "reaction" to the thing being done to them. 
    /// </summary>
    /// <param name="verb"></param>
    /// <param name="data"></param>
    public void Network_Respond(InteractVerbs verb, Character origin, InteractData data)
    {
        GameManager.Instance().AppendMessage("Interaction received from " + origin.name + "!");

        switch (verb)
        {
            case InteractVerbs.None:
                break;
            case InteractVerbs.Damage:
                // *** float: "damage"
                healthStatus.AlterHealth(-data.floatVal);
                //
                OnTakeDamage(-data.floatVal);

                // rb.velocity = new Vector3(0, data.floatVal, 0);

                // ??? <-- THIS IS VERY BAD PROGRAMMING.
                ((ControllableCharacter)this).ability.OnTakenDamage(origin, data.floatVal);
                break;
            case InteractVerbs.Tell:
                // *** string: "message"
                Helper.DisplayMessage(origin.name + ": \"" + data.stringVal + "\"");
                // Network_Interact(InteractVerbs.Tell, NetworkID, new InteractData("Ouch! You hit " + name+"!"));
                break;
        }
    }



    public virtual void OnTakeDamage(float amount)
    {

    }


    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //
        OnSpawned(LookVector);
    }
}


/// <summary>
/// Various passable data types for tossing over the network.
/// </summary>
public struct InteractData
{
    public int intVal;
    public float floatVal;
    public string stringVal;
    public Vector3 vector3Val;

    // ??? <-- The coupling is a bit loose here... I wonder if there's a better way to do this...?


    
    public InteractData(float val)
    {
        intVal = 0;
        floatVal = val;
        stringVal = "";
        vector3Val = Vector3.zero;
    }

    public InteractData(string val)
    {
        intVal = 0;
        floatVal = 0.0f;
        stringVal = val;
        vector3Val = Vector3.zero;
    }

    public InteractData(Vector3 val)
    {
        intVal = 0;
        floatVal = 0.0f;
        stringVal = "";
        vector3Val = val;
    }
}