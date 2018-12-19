using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// An "Entity" is something which simply "exists" within the context of the game. It doesn't need to move, be visible, or even DO anything!
/// This class helps describe some of the basic "attributes" of a thing which exists within the game.
/// </summary>
public class Entity : NetworkBehaviour {


    internal NetworkIdentity ni;


	public virtual void Awake()
    {
        ni = GetComponent<NetworkIdentity>();
        if (ni == null) { ni = gameObject.AddComponent<NetworkIdentity>(); }
    }
    public virtual void Start()
    {

    }


    private bool _discarded = false;
    public virtual void Discard()
    {
        if (!_discarded)
        {
            _discarded = true;

            Destroy(this.gameObject);
        }
    }

}
