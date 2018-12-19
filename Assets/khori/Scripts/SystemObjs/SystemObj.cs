using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * "SystemObjs" are a family of objects that do not particularly "exist" in the game.
 * Instead, they are used for managing various individual elements of the program/gameplay, much like a team of engineers making a machine work.
 * The "boss" of all the SystemObjs is the "GameManager", whom manages all the managers.
 * */
public class SystemObj : MonoBehaviour {

	// Use this for initialization
	public virtual void Start () {
        // *** DO NOT call your Initialize function unless the GameManager already exists!
        // *** This is because the GameManager calls everything else's Initialize function!
        // GameManager gm = GameManager.Instance();
        // if (gm != null) { Initialize(); }
	}



    internal virtual void Initialize()
    {

    }
}
