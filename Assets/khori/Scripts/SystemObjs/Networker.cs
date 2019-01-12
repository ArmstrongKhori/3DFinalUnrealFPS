using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Networker : MonoBehaviour {

    internal NetworkManager networkManager;
    internal NetworkManagerHUD networkHUD;
    //MIKE
    public GameObject SpawnedPlayer;

    private Scene scene;


    internal void Initialize()
    {
        networkManager = gameObject.AddComponent<NetworkManager>();
        scene = SceneManager.GetActiveScene();
        networkManager.offlineScene = scene.name;
        networkManager.onlineScene = scene.name;
        SpawnedPlayer = networkManager.playerPrefab = (GameObject)Resources.Load("Spawnables/player", typeof(GameObject));
        //
        foreach (GameObject obj in GameManager.Instance().networkSpawnablePrefabs)
        {
            // ClientScene.RegisterPrefab(obj);

            // Debug.Log(obj);
            networkManager.spawnPrefabs.Add(obj);
        }

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
