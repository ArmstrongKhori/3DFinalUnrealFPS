using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Networker : SystemObj {

    NetworkManager networkManager;
    NetworkManagerHUD networkHUD;

    private Scene scene;


    internal override void Initialize()
    {
        base.Initialize();
        //
        networkManager = gameObject.AddComponent<NetworkManager>();
        scene = SceneManager.GetActiveScene();
        networkManager.offlineScene = scene.name;
        networkManager.onlineScene = scene.name;
        networkManager.playerPrefab = (GameObject)Resources.Load("Spawnables/player", typeof(GameObject));
        networkManager.spawnPrefabs.Add(BattleManager.Instance().testObj);

        networkHUD = gameObject.AddComponent<NetworkManagerHUD>();
    }


    #region Singleton Stuff
    private static Networker _instance;
    public static Networker Instance() { return _instance; }
    private void Awake()
    {
        if (_instance == null || _instance == this)
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
