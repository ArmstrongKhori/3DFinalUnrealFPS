using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupRocketLauncher : MonoBehaviour {

    public float TheDistance = PlayerCasting.DistanceFromTarget;
    public GameObject TextDisplay;

    public GameObject FakeLauncher;
    public GameObject RealLauncher;


    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;

    }

    void OnMouseOver()
    {
        if (TheDistance <= 10)
        {
            TextDisplay.GetComponent<Text>().text = "Rocket Launcher";
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
            RealLauncher.SetActive(true);
            FakeLauncher.SetActive(false);
        }

    }
}
