using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeSR : MonoBehaviour {

    float zoom = 5;
    bool max = false;
    bool min = false;

	void Start () {
		
	}
	
	
	void Update () {

        
        gameObject.GetComponent<Camera>().fieldOfView = zoom;

        if (Input.mouseScrollDelta.y > 0 && !max)
        {
            zoom = zoom - 0.5f;
            min = false;
        }
        if (Input.mouseScrollDelta.y < 0 && !min)
        {
            zoom = zoom + 0.5f;
            max = false;
        }
        if (zoom == 1)
        {
            max = true;
        }
        if (zoom == 5)
        {
            min = true;
        }

        Debug.Log(zoom);
    }
}
