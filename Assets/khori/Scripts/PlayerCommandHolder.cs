using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCommandHolder : NetworkBehaviour {
    

    [Command]
    public void CmdSpawn(string name, NetworkInstanceId id, Vector3 lookVector)
    {
        // ??? <-- I hate that I have to send it my looking vector... What's with that??

        Actor a = BattleManager.Instance().Spawn(name, Helper.GetNetworkActor(id), lookVector);
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
