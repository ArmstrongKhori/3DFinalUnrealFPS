using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    internal MeshRenderer mr;

	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    internal bool isOn = true;
    public float respawnDelay = 8.0f;

    private float respawnTime = 0.0f;
    private void FixedUpdate()
    {
        if (!isOn)
        {
            respawnTime += Time.deltaTime;
            //
            if (respawnTime >= respawnDelay)
            {
                TurnOn();
            }
        }
    }

    public void TurnOn()
    {
        if (!isOn)
        {
            isOn = true;

            mr.enabled = true;
        }
    }
    public void TurnOff()
    {
        if (isOn)
        {
            isOn = false;

            respawnTime = 0f;
            mr.enabled = false;
        }
        
    }
}
