using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// A "Character" is an actor which interacts with projectiles, physics, or characters.
/// </summary>
public class Character : Actor
{
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


    public GameObject modelHolder;
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
        




        name = "PlayerNo_" + FindObjectsOfType<Character>().Length;
    }
    public override void Start()
    {
        base.Start();
        //

        if (isLocalPlayer)
        {
            //GameManager.Instance().DisplayMessage("You are " + name);
        }
    }



    public void Struck(Prop prop, StrikingData data)
    {
        // *** The owner of the prop is responsible for dealing the damage. If there's no owner, you (the target) are considered as its owner.
        Character c = (Character)prop.Owner;
        if (c == null) { c = this; }
        //
        GameManager.Instance().DisplayMessage(c.name + " interacts with " + name);
        c.Network_Interact(InteractVerbs.Damage, NetworkID, new InteractData(prop.power));
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
    public void Network_Interact(InteractVerbs verb, NetworkInstanceId id, InteractData data)
    {
        foreach (Character c in FindObjectsOfType<Character>())
        {
            if (c.NetworkID == id)
            {
                GameManager.Instance().AppendMessage("Found applicable character (" + c.name + ")");

                // *** What are we?
                if (isServer)
                {
                    GameManager.Instance().AppendMessage("Performing REMOTE call...");

                    c.RpcNetwork_Interact(verb, data);
                }
                else
                {
                    GameManager.Instance().AppendMessage("Performing COMMAND call...");

                    CmdNetwork_Interact(verb, data, id);
                }
                break;
            }
        }
    }

    [Command]
    public void CmdNetwork_Interact(InteractVerbs verb, InteractData data, NetworkInstanceId id) {
        GameManager.Instance().AppendMessage("Within COMMAND!");

        foreach (Character c in FindObjectsOfType<Character>())
        {
            if (c.NetworkID == id)
            {
                c.Network_Respond(verb, data);
            }
        }
    }
    [ClientRpc]
    public void RpcNetwork_Interact(InteractVerbs verb, InteractData data) {
        GameManager.Instance().AppendMessage("Within REMOTE!");
        Network_Respond(verb, data);
    }

    /// <summary>
    /// The player's "reaction" to the thing being done to them. 
    /// </summary>
    /// <param name="verb"></param>
    /// <param name="data"></param>
    public void Network_Respond(InteractVerbs verb, InteractData data)
    {
        GameManager.Instance().AppendMessage("Interaction received!");

        switch (verb)
        {
            case InteractVerbs.None:
                break;
            case InteractVerbs.Damage:
                // *** float: "damage"
                rb.velocity = new Vector3(0, data.floatVal, 0);
                break;
            case InteractVerbs.Tell:
                Network_Interact(InteractVerbs.Tell, NetworkID, new InteractData("Ouch! You hit " + name+"!"));
                break;
        }
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
    // ??? <-- The coupling is a bit loose here... I wonder if there's a better way to do this...?

    /// <summary>
    /// Used for passing a float value.
    /// </summary>
    /// <param name="val"></param>
    public InteractData(float val)
    {
        intVal = 0;
        floatVal = val;
        stringVal = "";
    }

    public InteractData(string val)
    {
        intVal = 0;
        floatVal = 0.0f;
        stringVal = val;
    }
}