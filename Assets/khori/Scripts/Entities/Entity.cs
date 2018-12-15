using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// An "Entity" is something which simply "exists" within the context of the game. It doesn't need to move, be visible, or even DO anything!
/// This class helps describe some of the basic "attributes" of a thing which exists within the game.
/// </summary>
public class Entity : NetworkBehaviour {





	public virtual void Awake()
    {

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
