using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupRifle : MonoBehaviour {

    public float TheDistance = PlayerCasting.DistanceFromTarget;
    public GameObject TextDisplay;

    public GameObject FakeRifle;
    public GameObject RealRifle;


    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;

    }

    void OnMouseOver()
    {
        if (TheDistance <= 10)
        {
            TextDisplay.GetComponent<Text>().text = "Laser Rifle";
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
            RealRifle.SetActive(true);
            FakeRifle.SetActive(false);
        }

    }
}
