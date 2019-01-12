using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnPointManager : MonoBehaviour {
    internal NetworkManager CurrentNetManager;
    internal GameObject[] BlueTeamSpawnPointList;
    internal GameObject[] RedTeamSpawnPointList;
	void Start () {
        LinkReferences();
    }


    public void LinkReferences()
    {
        CurrentNetManager = GameObject.Find("Networker").GetComponent<NetworkManager>();
        BlueTeamSpawnPointList = GameObject.FindGameObjectsWithTag("BlueTeamSpawnPoint");
        RedTeamSpawnPointList = GameObject.FindGameObjectsWithTag("RedTeamSpawnPoint");


        foreach (GameObject spc in BlueTeamSpawnPointList)
        {
            spc.AddComponent<NetworkStartPosition>();
        }
        foreach (GameObject spc in RedTeamSpawnPointList)
        {
            spc.AddComponent<NetworkStartPosition>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		//Decide team and spawn point for connected player 
	}



    public void Initialize()
    {
    }


    #region Singleton Stuff
    private static SpawnPointManager _instance;
    public static SpawnPointManager Instance() { return _instance; }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        //
        DontDestroyOnLoad(this.gameObject);


        Initialize();
    }
    #endregion

}
