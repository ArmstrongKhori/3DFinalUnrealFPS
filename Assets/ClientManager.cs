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

    private void Start()
    {
        NetworkHUD = NMScript.GetComponent<NetworkManagerHUD>();
        NetworkerScript = NMScript.GetComponent<Networker>();
    }

    private void Update()
    {
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
    
}
