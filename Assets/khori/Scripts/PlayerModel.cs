using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// A "programmatic simplification" of the "model" that each character comes with.
/// ... Errr, makes it easy to find points and joints.
/// </summary>
public class PlayerModel : NetworkBehaviour {

    public GameObject model;

    public List<NamePartPair> bodyParts;


    /// <summary>
    /// Returns the game object corresponding to the name.
    /// Useful for quickly locating and identifying the transforms on a model.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject Find(string name)
    {
        foreach (NamePartPair pair in bodyParts)
        {
            if (pair.name == name) { return pair.bodyPart; }
        }


        return null;
    }

}


[System.Serializable]
public class NamePartPair
{
    public string name;
    public GameObject bodyPart;
}