using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FPSController2 : NetworkBehaviour
{

    public const float RUNNINGMULT = 2.0f;

    public float SpeedLimit = 10f;

    private float ForwardSpeed = 5/3;
    private float BackwardSpeed = -4/3;
    private float StrafeSpeed = 5/3;
    // =====================================================================================
    // *** Default jump height was "100". Changed it to 10, just like in your scene.
    // *** This might also explain why your jumping physics went nuts in new scenes.
    public float JumpHeight = 10/3;
    // =====================================================================================

    private Vector3 Direction;

    // =====================================================================================
    // *** Made the mouse-looking x10 more sensitive... Was driving me nuts!
    private float SpeedH = 20;
    private float SpeedV = 20;
    // =====================================================================================

    public float pitch = 0.0f;
    public float yaw = 0.0f;
    public Vector3 LookVector { get { return new Vector3(pitch, yaw, 0.0f); } }

    private Vector3 moveDirection = Vector3.zero;

    public float Gravity = 9.81f;

    private bool isJumping = false;

    Rigidbody rb;

    private int CurrentGun;

    public List<bool> WeaponPickup;


    public Character character;

    private CharacterStateManager CharAnim;

    public GameObject gunHolder;

    public Transform[] WayPointPOS;
    private bool IsLauncherInHands = false;
    public float Speed;
    private int CurrWayPoint;

    public GameObject Grapplinghook;
    public GameObject GrappleHolder;


    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
        //
        CharAnim = character.stateManager;

        Grapplinghook = GameObject.Find("Hook");
        GrappleHolder = GameObject.Find("Hook Holder");


        WeaponPickup = new List<bool>();

        for (int i = 0; i < 5; i++)
        {
            WeaponPickup.Add(false);
        }



        gunHolder = GameManager.Instance().gunHolder;

        WayPointPOS[0] = Camera.main.transform.Find("WayPoints").Find("Point0");
        WayPointPOS[1] = Camera.main.transform.Find("WayPoints").Find("Point1");
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

            rb.velocity += transform.TransformDirection(new Vector3(0, 0, BackwardSpeed));
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Invoke("JumpAction", 0.2f);
            JumpAction();
        }

    }

    private void JumpAction()
    {

        if (!isJumping)
        {

            float temp = transform.position.y;
            isJumping = true;

            rb.velocity = new Vector3(rb.velocity.x, JumpHeight, rb.velocity.z);
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

    private void ControlAim()
    {
        yaw += SpeedH * Input.GetAxis("Mouse X");
        pitch -= SpeedV * Input.GetAxis("Mouse Y");

        
        // =====================================================================================
        // *** Separated the "turning" and the "looking" between the "body" and the "camera", respectively.
        transform.eulerAngles = new Vector3(0, LookVector.y, 0.0f);
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

    private void Idle()
    {
        CharAnim.Idle();
    }

    void AimWeapon()
    {
        if (Input.GetMouseButton(1) && !IsLauncherInHands)
        {
            CurrWayPoint = 1;
            gunHolder.transform.position = Vector3.MoveTowards(gunHolder.transform.position, WayPointPOS[CurrWayPoint].position, Speed);
            gunHolder.transform.rotation = WayPointPOS[CurrWayPoint].rotation;
        }
        else
        {

            CurrWayPoint = 0;
            gunHolder.transform.position = Vector3.MoveTowards(gunHolder.transform.position, WayPointPOS[CurrWayPoint].position, Speed);
            gunHolder.transform.rotation = WayPointPOS[CurrWayPoint].rotation;

        }
    }

    private void SelectWeapon()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            CurrentGun = 1;


            /*PickupManager.Weapons[0].SetActive(true);
            PickupManager.Weapons[1].SetActive(false);
            PickupManager.Weapons[2].SetActive(false);
            PickupManager.Weapons[3].SetActive(false);
            PickupManager.Weapons[4].SetActive(false);*/

            

            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            CurrentGun = 2;


            /*PickupManager.Weapons[0].SetActive(false);
            PickupManager.Weapons[1].SetActive(true);
            PickupManager.Weapons[2].SetActive(false);
            PickupManager.Weapons[3].SetActive(false);
            PickupManager.Weapons[4].SetActive(false);*/


            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            CurrentGun = 3;

            /*PickupManager.Weapons[0].SetActive(false);
            PickupManager.Weapons[1].SetActive(false);
            PickupManager.Weapons[2].SetActive(true);
            PickupManager.Weapons[3].SetActive(false);
            PickupManager.Weapons[4].SetActive(false);*/


            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            CurrentGun = 4;


            /*PickupManager.Weapons[0].SetActive(false);
            PickupManager.Weapons[1].SetActive(false);
            PickupManager.Weapons[2].SetActive(false);
            PickupManager.Weapons[3].SetActive(true);
            PickupManager.Weapons[4].SetActive(false);*/

            Debug.Log(CurrentGun);
        }

        if (Input.GetKey(KeyCode.Alpha5))
        {
            CurrentGun = 5;

            /*PickupManager.Weapons[0].SetActive(false);
            PickupManager.Weapons[1].SetActive(false);
            PickupManager.Weapons[2].SetActive(false);
            PickupManager.Weapons[3].SetActive(false);
            PickupManager.Weapons[4].SetActive(true);*/

            Debug.Log(CurrentGun);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer) { return; }
        
        if (character.stateManager.IsDead) { return; }

        Idle();
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
            rb.velocity = tempVelo.normalized * Mathf.Min(tempVelo.magnitude, SpeedLimit * RUNNINGMULT);
        }
        else
        {
            rb.velocity = tempVelo.normalized * Mathf.Min(tempVelo.magnitude, SpeedLimit);
        }
        // !!! <-- In the future, increasing the character's "friction" might be more "natural-looking" than just brick-walling their speed...
        //
        rb.velocity = new Vector3(rb.velocity.x, tempFall, rb.velocity.z);
        // =====================================================================================
        //
        SelectWeapon();

        Vector3 localVel = transform.InverseTransformVector(rb.velocity);

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
            
        }
        */
        
    }

    // Input logic for Animator!!! ----->
    #region
    void Update()
    {
        if (!isLocalPlayer) { return; }
        if(isJumping) { return; }


        CharAnim.InMotion = false;
        CharAnim.playerMotion.SetBool("IsShot", false);
        CharAnim.playerMotion.SetBool("IsSniperAim", false);

        // JUST FOR TESTING 

        //Walking and Run Forward -----> 
        if (Input.GetKey(KeyCode.W))
        {
            CharAnim.WalkingForward();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                CharAnim.RunningForward();
            }
        }

        //Walking and Running Backward -----> 
        if (Input.GetKey(KeyCode.S))
        {
            CharAnim.WalkingBackward();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                CharAnim.RunningBackward();
            }
        }

        //Walking and Running Left
        if (Input.GetKey(KeyCode.A))
        {
            CharAnim.WalkingLeft();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                CharAnim.RunningLeft();
            }
        }
        //Walking and Running Right
        if (Input.GetKey(KeyCode.D))
        {
            CharAnim.WalkingRight();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                CharAnim.RunningRight();
            }
        }

        //Walking Forward Right
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            CharAnim.WalkingForwardRight();
        }
        //Walking Forward Left
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            CharAnim.WalkingForwardLeft();
        }
        //Running Forward Right
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            CharAnim.RunningForwardRight();
        }
        //Running Forward Left
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            CharAnim.RunningForwardLeft();
        }
        //Walking Backward Right
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            CharAnim.WalkingBackwardRight();
        }
        //Walking Backward Left
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            CharAnim.WalkingBackwardLeft();
        }

        //Running Backward Right
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            CharAnim.RunningBackwardRight();
        }
        //Running Backward Left
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            CharAnim.RunningBackwardLeft();
        }

        //Shooting
        if (Input.GetKey(KeyCode.Mouse0))
        {
            CharAnim.Shooting();
        }

        //Sniper Aiming
        if (Input.GetKey(KeyCode.Mouse1))
        {
            CharAnim.SniperAim();
        }

        //Jumping -----> 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CharAnim.Jump();
        }


        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            CharAnim.Death();
        }


        if (CharAnim.IsActive && !CharAnim.IsDead && !CharAnim.InMotion)
        {
            // Debug.Log("IDLE TURNING BACK");
            Idle();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CharAnim.TurnToGunController(); 

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CharAnim.TurnToRifleController();
        }

        //
        if (Input.GetKey(KeyCode.Alpha6))
        {
            CharAnim.TakeLaserRifle();
            IsLauncherInHands = false;
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            CharAnim.TakeSniperRifle();
            IsLauncherInHands = false;
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            CharAnim.TakeGranadeLauncher();
            IsLauncherInHands = true;
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            CharAnim.TakeGun();
            IsLauncherInHands = false;
        }
        AimWeapon();
    }
    #endregion

}
