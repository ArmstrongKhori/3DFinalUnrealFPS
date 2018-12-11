using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {

    private float ForwardSpeed = 5;
    private float BackwardSpeed = -4;
    private float StrafeSpeed = 5;
    public float JumpHeight = 10;

    private Vector3 Direction;

    private float SpeedH = 2;
    private float SpeedV = 2;

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    //private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;

    public float Gravity = 9.81f;

    private bool isJumping = false;

    Rigidbody rb;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
    }
	
    private void MoveForward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * ForwardSpeed * Time.deltaTime);
        }
    }

    private void MoveBackward()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * BackwardSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isJumping)
            {
                float temp = transform.position.y;
                isJumping = true;
                //rb.AddForce(0, JumpHeight, 0);

                transform.Translate(Vector3.up * JumpHeight * Time.deltaTime);//<<<ERICK_HOBBS

                //Debug.Log(transform.position.y);
            }
        }

        if (GetComponent<Rigidbody>().velocity.y == 0)
        {
            isJumping = false;

            //Debug.Log(transform.position.y);
        }
    }

    private void Strafe()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * StrafeSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.right * -StrafeSpeed * Time.deltaTime);
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

    void FixedUpdate()
    {
           transform.Translate(Vector3.down * 2 * Time.deltaTime);//<<<ERICK_HOBBS
    }
}
