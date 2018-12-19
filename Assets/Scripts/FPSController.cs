﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {

    private float ForwardSpeed = 5;
    private float BackwardSpeed = -4;
    private float StrafeSpeed = 5;
    private float JumpHeight = 10;

    private Vector3 Direction;

    private float SpeedH = 2;
    private float SpeedV = 2;

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    //private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;

    private bool isJumping = false;

    Rigidbody rb;

    private int CurrentGun;

    private List<bool> WeaponPickup;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();

        WeaponPickup = new List<bool>();

        for (int i = 0; i < 5; i++)
        {
            WeaponPickup.Add(false);
        }

    }
	
    private void MoveForward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += transform.TransformDirection(new Vector3(0, 0, ForwardSpeed));
        }
    }

    private void MoveBackward()
    {
        if (Input.GetKey(KeyCode.S))
        {         
            rb.velocity += transform.TransformDirection(new Vector3(0, 0, BackwardSpeed));
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
                rb.velocity = new Vector3(rb.velocity.x, JumpHeight, rb.velocity.z);
            }
        }

    }

    private void Strafe()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += transform.TransformDirection(new Vector3(StrafeSpeed, 0, 0));
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += transform.TransformDirection(new Vector3(-StrafeSpeed, 0, 0));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

    private void ControlAim()
    {
        yaw += SpeedH * Input.GetAxis("Mouse X");
        pitch -= SpeedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    private void SelectWeapon()
    {
        /*
        if (Input.GetKey(KeyCode.Alpha1) && )
        {
            CurrentGun = 1;

            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha2) && )
        {
            CurrentGun = 2;
            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha3) && )
        {
            CurrentGun = 3;
            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha4) && )
        {
            CurrentGun = 4;
            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha5) && )
        {
            CurrentGun = 5;
            Debug.Log(CurrentGun);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        ControlAim();
        MoveForward();
        MoveBackward();
        Strafe();
        Jump();
        SelectWeapon();

    }
}
