using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An "Actor" is something which has the attributes to... Well, "act".
/// Its behavior is dynamic and does not need to be manipulated by anything else to operate.
/// </summary>
public class Actor : Entity {

    internal Rigidbody rb;

    public virtual Vector3 LookVector { get { return transform.forward; } }


    public override void Awake()
    {
        base.Awake();
        //
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null) { rb = gameObject.AddComponent<Rigidbody>(); }
    }


    public virtual void Act() { }


    public void Despawn()
    {

        //
        OnDespawned();
    }


    public virtual void OnSpawned(Actor by = null) { }
    public virtual void OnDespawned() { }
}
