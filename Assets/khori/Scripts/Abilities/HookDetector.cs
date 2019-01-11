using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject HookedObject;
    public bool hooked = false;

    private void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable")
        {
            Debug.Log("Hit Hookable Object");
            hooked = true;
            HookedObject = other.gameObject;
        }
    }
}
