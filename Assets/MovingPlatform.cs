using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public GameObject Pos1;
    public GameObject Pos2;
    public bool Up;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //move platform up
        if (Up)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, Pos1.transform.position, Time.deltaTime * 1);
        }
        //move platform down
        if (!Up)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, Pos2.transform.position  , Time.deltaTime * 1);
        }
        //check if platform has reached first pos
        if(transform.position == Pos1.transform.position)
        {
            Up = false;
        }
        //checked if its reached the second pos
        if (transform.position == Pos2.transform.position)
        {
            Up = true;
        }
    }
}
