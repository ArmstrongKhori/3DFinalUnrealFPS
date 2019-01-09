using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {

    public GameObject hook;
    public GameObject hookHolder;

    //speed of hook when fired
    public float hookTravelSpeed;

    //player speed when hooked to object
    public float playerTravelSpeed;

    public static bool fired;
    public static bool hooked;

    //max distance hook can travel
    public float maxDistance;

    //dynamic float that will be detected before maxDistance is reached
    private float currentDistance;

    void Update()
    {
        // firing hook
        if (Input.GetMouseButtonDown(1) && fired == false)
            fired = true;

        if (fired == true)
        {
            // moved hook towards aimed point
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);

            //calculate the distance between the hook and player so hook doesnt travel to far
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);
        }
    }


}
