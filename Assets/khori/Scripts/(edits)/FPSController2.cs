using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController2 : MonoBehaviour
{

    private float ForwardSpeed = 5;
    private float BackwardSpeed = -4;
    private float StrafeSpeed = 5;
    // =====================================================================================
    // *** Default jump height was "100". Changed it to 10, just like in your scene.
    // *** This might also explain why your jumping physics went nuts in new scenes.
    public float JumpHeight = 10;
    // =====================================================================================

    private Vector3 Direction;

    // =====================================================================================
    // *** Made the mouse-looking x10 more sensitive... Was driving me nuts!
    private float SpeedH = 20;
    private float SpeedV = 20;
    // =====================================================================================

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    //private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;

    public float Gravity = 9.81f;

    private bool isJumping = false;

    Rigidbody rb;

    private int CurrentGun;

    private List<bool> WeaponPickup;

    private CharacterStateManager CharAnim;

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

        CharAnim = GetComponent<CharacterStateManager>();
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
            CharAnim.playerMotion.SetBool("IsJump", false);
            isJumping = false;
        }
    }

    public Camera attachedCamera;

    private void ControlAim()
    {
        yaw += SpeedH * Input.GetAxis("Mouse X");
        pitch -= SpeedV * Input.GetAxis("Mouse Y");

        // =====================================================================================
        // *** Separated the "turning" and the "looking" between the "body" and the "camera", respectively.
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        attachedCamera.transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0.0f);
        // =====================================================================================
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
    void FixedUpdate()
    {
        ControlAim();
        MoveForward();
        MoveBackward();
        Strafe();
        // =====================================================================================
        // ??? <-- Beware of the "Half-Life" bug!
        // One can move faster than normal by walking and strafing simultaneously. You need to multiply movement speed by "0.7" when both are being done at the same time to prevent this.
        // =====================================================================================
        Jump();
        //
        // =====================================================================================
        // *** Movement friction that ignores up/down velocity.
        // *** This MUST be done after all other movements are completed!
        Vector3 tempVelo = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        float tempFall = rb.velocity.y;
        //
        rb.velocity = tempVelo.normalized * Mathf.Min(tempVelo.magnitude, 10f);
        // !!! <-- In the future, increasing the character's "friction" might be more "natural-looking" than just brick-walling their speed...
        //
        rb.velocity = new Vector3(rb.velocity.x, tempFall, rb.velocity.z);
        // =====================================================================================
        //
        SelectWeapon();
        Debug.Log(rb.velocity);

    }
}
