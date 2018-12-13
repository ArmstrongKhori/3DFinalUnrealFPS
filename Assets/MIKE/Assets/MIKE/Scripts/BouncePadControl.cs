using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadControl : MonoBehaviour {

    // Use this for initialization
    private void OnCollisionEnter(Collision collision)
    {
     if (collision.gameObject.tag == "Player")
        {
            // BOUNCE PLAYER 2X HEIGHT
            //collision.gameObject.GetComponent<>
        }   
    }
}
