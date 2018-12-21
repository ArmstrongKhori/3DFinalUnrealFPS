using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour {


    public GameObject gunHolder;
    public Transform wayPointsContainer;
    //
    public Transform pointLeft1;
    public Transform pointLeft2;
    //
    public Transform pointCenter;
    //
    public Transform pointRight1;
    public Transform pointRight2;


    private float Speed = 0.005f;
    

    public bool IsMovingLeft = true;
    //
    public bool IsMovingRight = false;

    private void Start()
    {
        
    }


    void Wave()
    {
        if (IsMovingLeft)
        {
            gunHolder.transform.Translate(-0.005f, 0.005f, 0);
            IsMovingRight = true;


        }
        if (IsMovingRight)
        {
            gunHolder.transform.Translate(+0.005f, -0.005f, 0);
            IsMovingLeft = true;
        }

    }



    void Update () {


        if (Input.GetKey(KeyCode.W) || IsMovingLeft && Input.GetKey(KeyCode.S) || IsMovingLeft && Input.GetKey(KeyCode.A) || IsMovingLeft && Input.GetKey(KeyCode.D))
        {
            Wave();

        }


        wayPointsContainer.transform.position = this.transform.position;

    }

}
