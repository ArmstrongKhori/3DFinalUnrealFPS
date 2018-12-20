using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupMelee : MonoBehaviour {

    public float TheDistance = PlayerCasting.DistanceFromTarget;
    public GameObject TextDisplay;

    public GameObject FakeMelee;
    public GameObject RealMelee;


    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;

    }

    void OnMouseOver()
    {
        if (TheDistance <= 10)
        {
            TextDisplay.GetComponent<Text>().text = "Busted Axe";
        }

    }

    void OnMouseExit()
    {
        TextDisplay.GetComponent<Text>().text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.position = new Vector3(0, -1000, 0);
            RealMelee.SetActive(true);
            FakeMelee.SetActive(false);
        }

    }
}
