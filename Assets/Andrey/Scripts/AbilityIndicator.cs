using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AbilityIndicator : NetworkBehaviour
{

    public GameObject indicator;
    public Transform indicatorTransform;


    private void Start()
    {
        gameObject.name = "Shield";
        
    }


    void Update() {



        if (indicator.activeSelf == true)
        {
            indicatorTransform.Rotate(0, -0.1f, 0, 0);
            indicator.transform.Rotate(0, 5f, 0, Space.World);

        }

        

    }

}
