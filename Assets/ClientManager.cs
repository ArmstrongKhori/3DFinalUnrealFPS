using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class ClientManager : SystemObj
{
    internal NetworkManager NMScript;
    internal Networker NetworkerScript;
    internal NetworkManagerHUD NetworkHUD;
    public int ConnectedPlayers;
    internal float cTimer;
    private int prevCount = 0;
    internal Button StartGameBtn;
    internal Button ReadyBtn;
    public NetworkIdentity PlayerNetworkIdentity;




    public override void Start()
    {
        readyDictionary = new Dictionary<NetworkInstanceId, bool>();

        NetworkHUD = NMScript.GetComponent<NetworkManagerHUD>();
        NetworkerScript = NMScript.GetComponent<Networker>();
        StartGameBtn = GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>();
        ReadyBtn = GameObject.FindGameObjectWithTag("ReadyButton").GetComponent<Button>();
        PlayerNetworkIdentity = NetworkerScript.SpawnedPlayer.GetComponent<NetworkIdentity>();
        HideButtons();
        //
        //
        StartGameBtn.onClick.AddListener(buttonEvents_Start);
        ReadyBtn.onClick.AddListener(buttonEvents_Ready);
    }


    public void buttonEvents_Start()
    {
        Debug.Log("START");
        //
        foreach (KeyValuePair<NetworkInstanceId, bool> pair in readyDictionary)
        {
            DontDestroyOnLoad(Helper.GetNetworkPlayer(pair.Key).gameObject);
        }
        //
        //
        SceneManager.LoadScene(1);
        //
        
    }
    public void buttonEvents_Ready()
    {
        Debug.Log("READY");

    }


    public void HideButtons()
    {
        ReadyBtn.gameObject.SetActive(false);
        StartGameBtn.gameObject.SetActive(false);
    }
    public void ShowButtons(bool isAdmin)
    {

        Debug.Log("4. Show buttons.");
        ReadyBtn.gameObject.SetActive(true);
        //
        StartGameBtn.gameObject.SetActive(isAdmin);
    }




    // Player - <readyStatus>
    public Dictionary<NetworkInstanceId, bool> readyDictionary;

    internal void PlayerConnected(NetworkInstanceId id, bool areEntering)
    {
        /*
        ControllableCharacter cc = Helper.GetNetworkPlayer(id);
        //
        readyDictionary.Add(id, false);
        */
    }
    internal void PlayerReadied(NetworkInstanceId id, bool areTheyReady)
    {
        readyDictionary[id] = areTheyReady;
    }
    private void Update()
    {
        /*
        if (PlayerNetworkIdentity.isServer)
        {
            Debug.Log("Server");
            //  ReadyBtn.SetActive(false);
            //  StartGameBtn.SetActive(true);
        }
        else if (!PlayerNetworkIdentity.isServer)
        {
            Debug.Log("Client");

            //  ReadyBtn.SetActive(true);
            //  StartGameBtn.SetActive(false);
        }
        */

        cTimer += Time.deltaTime;
        if (cTimer >= .5f)
        {
            cTimer = 0;
            ClientCheck();
        }
    }
    public void ClientCheck()
    {
        foreach (ControllableCharacter cc in FindObjectsOfType<ControllableCharacter>())
        {
            if (!readyDictionary.ContainsKey(cc.NetworkID))
            {
                readyDictionary.Add(cc.NetworkID, false);
                //
                Helper.GetLocalPlayer().PCH.RpcLobbyShowButtons(cc.isServer);
            }
        }


        /*
        ConnectedPlayers = NetworkServer.connections.Count;
        int newCount = ConnectedPlayers;

        if (newCount > prevCount)
        {
            prevCount = newCount;
            PlayercConnected();
        }
        if (newCount < prevCount)
        {
            prevCount = newCount;
            PlayercDisconnected();
        }
        */
    }

    /*
    public void PlayercConnected()
    {
        Debug.Log("PlayerConnected");

    }
    public void PlayercDisconnected()
    {
        Debug.Log("PlayerDisconnected");
    }


    */



    #region Singleton Stuff
    private static ClientManager _instance;
    public static ClientManager Instance() { return _instance; }

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
