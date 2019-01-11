using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Interactable {

    public int healAmount = 100;

    void OnTriggerEnter(Collider other)
    {
        if (!isOn) { return; }


        PlayerHealth2 player = other.GetComponent<PlayerHealth2>();

        if(player != null)
        {
            player.AlterHealth(healAmount);

            TurnOff();
        }
    }
}
