using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/// <summary>
/// A "separator" for network-type functions (aka: "Commands" and Remote-Procedure Calls)
/// </summary>
public class PlayerCommandHolder : NetworkBehaviour {



    [Command]
    public void CmdNetwork_Interact(Character.InteractVerbs verb, NetworkInstanceId from, NetworkInstanceId to, InteractData data)
    {
        GameManager.Instance().AppendMessage("Within COMMAND!");

        foreach (ControllableCharacter c in FindObjectsOfType<ControllableCharacter>())
        {
            if (c.isClient)
            {
                c.pch.RpcNetwork_Interact(verb, from, to, data);
            }
        }
    }
    [ClientRpc]
    public void RpcNetwork_Interact(Character.InteractVerbs verb, NetworkInstanceId from, NetworkInstanceId to, InteractData data)
    {
        GameManager.Instance().AppendMessage("Within REMOTE!");


        Character cFrom = Helper.GetNetworkActor(from) as Character;
        Character cTo = Helper.GetNetworkActor(to) as Character;
        //
        cTo.Network_Respond(verb, cFrom, data);
    }




    [Command]
    public void CmdSpawn(string name, NetworkInstanceId id, Vector3 lookVector)
    {
        Debug.Log("doing the spawn!    " + id);


        // ??? <-- I hate that I have to send it my looking vector... What's with that??

        Actor a = BattleManager.Instance().Spawn(name, Helper.GetNetworkActor(id), lookVector);
        //
        Debug.Log(a);
        //
        NetworkServer.Spawn(a.gameObject);
    }




    [Command]
    public void CmdCreateBulletTrail(Vector3 start, Vector3 end, float sz, Color col)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load("Spawnables/BulletTrail", typeof(GameObject)), start, Quaternion.LookRotation(end - start));
        // GameObject go = (GameObject)Instantiate(Resources.Load("Spawnables/testpref", typeof(GameObject)), start, Quaternion.LookRotation(end - start));
        //
        BulletTrail comp = go.GetComponent<BulletTrail>();
        comp.startPoint = start;
        comp.endPoint = end;
        comp.size = sz;
        comp.color = col;
        //
        //
        NetworkServer.Spawn(go);
        //
        //
        comp.RpcOnServerSpawned();
    }

}
