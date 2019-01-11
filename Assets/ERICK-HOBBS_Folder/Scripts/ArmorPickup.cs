using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : Interactable {

    public int armorAmount = 100;

    void OnTriggerEnter(Collider other)
    {
        if (!isOn) { return; }


        PlayerArmor player = other.GetComponent<PlayerArmor>();

        if (player != null)
        {
            player.AlterArmor(armorAmount);

            TurnOff();
        }
    }
}
