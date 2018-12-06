using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {

    public Camera pCamera;

    public float ForwardSpeed;
    public float BackwardSpeed;
    public float StrafeSpeed;

    private Vector3 Direction;

    private float SpeedH = 2;
    private float SpeedV = 2;

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    private int Health;
    //private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;

    public float Gravity = 9.81f;

    private bool isJumping = false;

    //Cursor.Visable = false; - hides cursor.

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        //controller = GameObject.Find("Capsule").GetComponent<CharacterController>();
    }
	
    private void MoveForward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }
    }

    private void MoveBackward()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * -5 * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isJumping)
            {
                isJumping = true;
                transform.Translate(Vector3.up * 10 * Time.deltaTime);
            }
        }

        if (GetComponent<Rigidbody>().velocity.y == 0)
        {
            isJumping = false;
        }
    }

    private void Strafe()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 5 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.right * -5 * Time.deltaTime);
        }
    }

    private void ControlAim()
    {
        yaw += SpeedH * Input.GetAxis("Mouse X");
        pitch -= SpeedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }



    // Update is called once per frame
    void Update()
    {
        ControlAim();
        MoveForward();
        MoveBackward();
        Strafe();
        Jump();
    }
}
