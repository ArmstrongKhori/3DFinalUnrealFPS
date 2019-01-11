using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("PlayerNo_1");
    }

    private void Update()
    {
        if(player == null)
        {
            player = GameObject.Find("PlayerNo_1");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable")
        {
            if (player != null)
            {
                player.GetComponent<GrapplingHook>().hooked = true;
                player.GetComponent<GrapplingHook>().hookedObj = other.gameObject;
            }
        }
    }

}
