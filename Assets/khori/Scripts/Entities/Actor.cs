using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An "Actor" is something which has the attributes to... Well, "act".
/// Its behavior is dynamic and does not need to be manipulated by anything else to operate.
/// </summary>
public class Actor : Entity {

    internal Rigidbody rb;
    /// <summary>
    /// This is not always necessary, but you can describe who "owns" this actor.
    /// If it is null, the actor is considered as owning itself.
    /// </summary>
    public Actor Owner { get { return owner == null ? this : owner; } set { owner = value; } }
    private Actor owner;

    public virtual Vector3 LookVector { get { return transform.forward; } }


    public override void Awake()
    {
        base.Awake();
        //
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null) { rb = gameObject.AddComponent<Rigidbody>(); }


        BattleManager bm = BattleManager.Instance();
        if (bm != null)
        {
            bm.allActors.Add(this);
            // ??? <-- Fix this logic later...
        }
    }


    public virtual void Act() { }


    public void Despawn()
    {
        OnDespawned();
        //
        Discard();
    }

    public override void Discard()
    {
        base.Discard();
        //
        BattleManager.Instance().allActors.Remove(this);
        // ??? <-- Fix this, zzzz...
    }


    public virtual void OnSpawned() { }
    public virtual void OnDespawned() { }
}
