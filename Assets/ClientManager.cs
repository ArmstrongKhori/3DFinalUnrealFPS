using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ClientManager : MonoBehaviour {
    public NetworkManager NMScript;
    public Networker NetworkerScript;
    public NetworkManagerHUD NetworkHUD;
    public int ConnectedPlayers;
    public float cTimer;
    private int prevCount = 0;
    public GameObject StartGameBtn;
    public GameObject ReadyBtn;
    public NetworkIdentity PlayerNetworkIdentity;

    private void Start()
    {
        NetworkHUD = NMScript.GetComponent<NetworkManagerHUD>();
        NetworkerScript = NMScript.GetComponent<Networker>();
        StartGameBtn = GameObject.FindGameObjectWithTag("StartButton");
        ReadyBtn = GameObject.FindGameObjectWithTag("ReadyButton");
        HideButtons();
        PlayerNetworkIdentity = NetworkerScript.SpawnedPlayer.GetComponent<NetworkIdentity>();
    }

    private void Update()
    {
       if (PlayerNetworkIdentity.isServer)
       {
            Debug.Log("Server");
         //  ReadyBtn.SetActive(false);
         //  StartGameBtn.SetActive(true);
       }else if (!PlayerNetworkIdentity.isServer)
        {
            Debug.Log("Client");

            //  ReadyBtn.SetActive(true);
            //  StartGameBtn.SetActive(false);
        }


        cTimer += Time.deltaTime;
        if (cTimer >= .5f)
        {
            cTimer = 0;
            ClientCheck();
        }
    }

    public void ClientCheck()
    {
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
    }
        public void PlayercConnected()
    {
        Debug.Log("PlayerConnected");

    }
    public void PlayercDisconnected()
    {
        Debug.Log("PlayerDisconnected");
    }


    private void HideButtons()
    {
        ReadyBtn.SetActive(false);
        StartGameBtn.SetActive(false);
    }
    private void ShowButtons()
    {
        ReadyBtn.SetActive(true);
        StartGameBtn.SetActive(true);
    }
}
