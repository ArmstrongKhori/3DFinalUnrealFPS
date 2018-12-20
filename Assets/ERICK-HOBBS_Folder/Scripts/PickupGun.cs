using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupGun : MonoBehaviour {

    public float TheDistance = PlayerCasting.DistanceFromTarget;
    public GameObject TextDisplay;

    public GameObject FakeGun;
    public GameObject RealGun;
    

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
        
    }

    void OnMouseOver()
    {
        if (TheDistance <= 10)
        {
            TextDisplay.GetComponent<Text>().text = "Hand Gun";
        }

       /*if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 5)
            {
                StartCoroutine(TakeHandgun());
            }
        }*/
    }

    void OnMouseExit()
    {
        TextDisplay.GetComponent<Text>().text = "";
    }

    /*IEnumerator TakeHandgun()
    {
        transform.position = new Vector3(0, -1000, 0);
        FakeGun.SetActive(false);
        RealGun.SetActive(true);
        yield return new WaitForSeconds(0.1f);
    }*/
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            transform.position = new Vector3(0, -1000, 0);
            RealGun.SetActive(true);
            FakeGun.SetActive(false);
        }
        
    }
}
