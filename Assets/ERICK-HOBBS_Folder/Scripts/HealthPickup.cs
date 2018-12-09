using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public int healAmount = 10;

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if(player != null)
        {
            player.AlterHealth(healAmount);
            gameObject.SetActive(false);
        }
    }
}
