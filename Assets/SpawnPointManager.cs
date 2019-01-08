using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnPointManager : MonoBehaviour {
    public NetworkManager CurrentNetManager;
    public GameObject[] BlueTeamSpawnPointList;
    public GameObject[] RedTeamSpawnPointList;
	void Start () {
        CurrentNetManager = GameObject.Find("Networker").GetComponent<NetworkManager>();
        BlueTeamSpawnPointList = GameObject.FindGameObjectsWithTag("BlueTeamSpawnPoint");
        RedTeamSpawnPointList = GameObject.FindGameObjectsWithTag("RedTeamSpawnPoint");
	}
	
	// Update is called once per frame
	void Update () {
		//Decide team and spawn point for connected player 
	}
}
