using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ClientManager : MonoBehaviour {
    public NetworkManager NMScript;
    public Networker NetworkerScript;
    public int ConnectedPlayers;
    public float cTimer;
    private int prevCount = 0;

    private void Start()
    {
        NetworkerScript = NMScript.GetComponent<Networker>();
    }

    private void Update()
    {
        ConnectedPlayers = NetworkServer.connections.Count;
        cTimer += Time.deltaTime;

        if (cTimer >= 1)
        {
            cTimer = 0;
            ClientCheck();
        }
    }
    public void ClientCheck()
    {
        int newCount = ConnectedPlayers;

        if (newCount > prevCount)
        {
            prevCount = newCount;
            PlayercConnected();
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
