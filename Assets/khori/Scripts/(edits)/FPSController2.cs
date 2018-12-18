using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FPSController2 : NetworkBehaviour
{

    public const float RUNNINGMULT = 2.0f;

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

    private int CurrentGun = 1;

    private int GetCurrentGun() { return CurrentGun; }

    public List<bool> WeaponPickup;

    public Character character;

    private CharacterStateManager CharAnim;

    private WeaponPickupManager PickupManager;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
        //
        CharAnim = character.stateManager;

        PickupManager = GetComponent<WeaponPickupManager>();


        WeaponPickup = new List<bool>();

        for (int i = 0; i < 5; i++)
        {
            WeaponPickup.Add(false);
        }
        WeaponPickup[0] = true;



        Debug.Log(WeaponPickup);
        if (!isLocalPlayer) { attachedCamera.gameObject.SetActive(false); }
    }

    private void MoveForward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity += transform.TransformDirection(new Vector3(0, 0, ForwardSpeed* RUNNINGMULT));
            }
            else
            {
                rb.velocity += transform.TransformDirection(new Vector3(0, 0, ForwardSpeed));
            }
         }
    }

    private void MoveBackward()
    {
        if (Input.GetKey(KeyCode.S))
        {
            CharAnim.WalkingBackward();
            rb.velocity += transform.TransformDirection(new Vector3(0, 0, BackwardSpeed));
            if (Input.GetKey(KeyCode.LeftShift))
            {
                CharAnim.RunningBackward();
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isJumping)
            {
                CharAnim.Jump();

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
            CharAnim.WalkingRight();
            rb.velocity += transform.TransformDirection(new Vector3(StrafeSpeed, 0, 0));
        }

        if (Input.GetKey(KeyCode.A))
        {
            CharAnim.WalkingLeft();
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

        if (pitch > 80)
        {
            pitch = 80;
        }
        else if (pitch < -30)
        {
            pitch = -30;
        }
    }

    private void SelectWeapon()
    {
        
        if (Input.GetKey(KeyCode.Alpha1) && WeaponPickup[0] == true)
        {
            CurrentGun = 1;

            PickupManager.Weapons[0].SetActive(true);
            PickupManager.Weapons[1].SetActive(false);
            PickupManager.Weapons[2].SetActive(false);
            PickupManager.Weapons[3].SetActive(false);
            PickupManager.Weapons[4].SetActive(false);

            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha2) && WeaponPickup[1] == true)
        {
            CurrentGun = 2;

            PickupManager.Weapons[0].SetActive(false);
            PickupManager.Weapons[1].SetActive(true);
            PickupManager.Weapons[2].SetActive(false);
            PickupManager.Weapons[3].SetActive(false);
            PickupManager.Weapons[4].SetActive(false);

            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha3) && WeaponPickup[2] == true)
        {
            CurrentGun = 3;

            PickupManager.Weapons[0].SetActive(false);
            PickupManager.Weapons[1].SetActive(false);
            PickupManager.Weapons[2].SetActive(true);
            PickupManager.Weapons[3].SetActive(false);
            PickupManager.Weapons[4].SetActive(false);

            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha4) && WeaponPickup[3] == true)
        {
            CurrentGun = 4;

            PickupManager.Weapons[0].SetActive(false);
            PickupManager.Weapons[1].SetActive(false);
            PickupManager.Weapons[2].SetActive(false);
            PickupManager.Weapons[3].SetActive(true);
            PickupManager.Weapons[4].SetActive(false);

            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha5) && WeaponPickup[4] == true)
        {
            CurrentGun = 5;

            PickupManager.Weapons[0].SetActive(false);
            PickupManager.Weapons[1].SetActive(false);
            PickupManager.Weapons[2].SetActive(false);
            PickupManager.Weapons[3].SetActive(false);
            PickupManager.Weapons[4].SetActive(true);

            Debug.Log(CurrentGun);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer) { return; }

        
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = tempVelo.normalized * Mathf.Min(tempVelo.magnitude, 10f * RUNNINGMULT);
        }
        else
        {
            rb.velocity = tempVelo.normalized * Mathf.Min(tempVelo.magnitude, 10f);
        }
        // !!! <-- In the future, increasing the character's "friction" might be more "natural-looking" than just brick-walling their speed...
        //
        rb.velocity = new Vector3(rb.velocity.x, tempFall, rb.velocity.z);
        // =====================================================================================
        //
        SelectWeapon();
        Debug.Log(rb.velocity);

        Vector3 localVel = transform.InverseTransformVector(rb.velocity);
        CharAnim.playerMotion.SetFloat("Speed", localVel.x / ForwardSpeed /4);
        CharAnim.playerMotion.SetFloat("Direction", localVel.z / StrafeSpeed /4);

        /*
        if (localVel.z > 0)
        {
            CharAnim.WalkingForward();
        }
        else if (localVel.z < 0)
        {
            CharAnim.WalkingBackward();
        }
        else
        {
            CharAnim.Idle();
        }
        */

    }
}
