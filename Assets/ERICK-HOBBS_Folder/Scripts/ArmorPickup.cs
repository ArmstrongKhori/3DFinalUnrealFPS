using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : MonoBehaviour {

    public int armorAmount = 10;

    void OnTriggerEnter(Collider other)
    {
        PlayerArmor player = other.GetComponent<PlayerArmor>();

        if (player != null)
        {
            player.AlterArmor(armorAmount);
            gameObject.SetActive(false);
        }
    }
}
