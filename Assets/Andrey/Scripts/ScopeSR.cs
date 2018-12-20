using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeSR : MonoBehaviour {

    float zoom = 60;
    bool max = false;
    bool min = false;

	void Start () {
		
	}
	
	
	void Update () {

        
        gameObject.GetComponent<Camera>().fieldOfView = zoom;

        if (Input.mouseScrollDelta.y > 0 && !max)
        {
            zoom = zoom - 10;
            min = false;
        }
        if (Input.mouseScrollDelta.y < 0 && !min)
        {
            zoom = zoom + 10;
            max = false;
        }
        if (zoom == 10)
        {
            max = true;
        }
        if (zoom == 60)
        {
            min = true;
        }

        Debug.Log(zoom);
    }
}
