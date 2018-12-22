using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class playerguy : MonoBehaviour
{

    public Ability abilityThingy;
    
}



public class Ability : NetworkBehaviour {
    

    public virtual void OnFiveKills()
    {

    }


}
