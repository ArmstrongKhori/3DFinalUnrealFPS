using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterStateManager : NetworkBehaviour {


    public bool IsActive = false;
    public bool IsDead = false;
    public bool InMotion = false;


    public Animator playerMotion;

    public RuntimeAnimatorController controllerGun2D;
    public RuntimeAnimatorController controllerRifle2D;


    private float Direction = 0.0f;
    private float Speed = 0.0f;
    private float animAdjNumber = 0.3f; // number which effects how fast switching between states

    private void Awake()
    {
       playerMotion.SetFloat("Speed", 0);
       playerMotion.SetFloat("Direction", 0);
       IsActive = true;
    }

    // FOR 1D BLEND TREE
    #region ANIMATIONS METHODS ARE HERE
    /*
        //IDLE
        public void Idle()
        {
            IsActive = true;
            IsDead = false;
            Debug.Log("IDLE STARTS HERE");
            playerMotion.SetFloat("Blend", 0.0f);
            playerMotion.SetBool("IsJump", false);
        }

        //WALKING FORWARD
        public void WalkingForward() {

            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Blend", 0.4f);
            InMotion = true;
        }

        //WALKING LEFT
        public void WalkingLeft()
        {
            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Blend", 0.55f);
            InMotion = true;
        }

        //WALKING RIGHT
        public void WalkingRight()
        {
            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Blend", 0.25f);
            InMotion = true;
        }

        //WALKING FORWARD LEFT
        public void WalkingForwardLeft()
        {
            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Blend", 0.47f);
            InMotion = true;
        }

        //WALKING FORWARD RIGHT
        public void WalkingForwardRight()
        {
           Debug.Log("WALKING STARTS HERE");
           playerMotion.SetFloat("Blend", 0.32f);
            InMotion = true;
        }


        //WALKING BACKWARD
        public void WalkingBackward()
        {
            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Blend", 0.1f);
            InMotion = true;
        }

        //RUNNING FORWARD
        public void RunningForward()
        {
            Debug.Log("HERE CHARACTER RUNS");
            playerMotion.SetFloat("Blend", 1.5f);
            InMotion = true;
        }

        //RUNNING LEFT
        public void RunningLeft()
        {
            Debug.Log("HERE CHARACTER RUNS");
            playerMotion.SetFloat("Blend", 1.5f);
            InMotion = true;
        }

        //RUNNING RIGHT
        public void RunningRight()
        {
            Debug.Log("HERE CHARACTER RUNS");
            playerMotion.SetFloat("Blend", 1.5f);
            InMotion = true;
        }
        //RUNNING FORWARD RIGHT
        public void RunningForwardRight()
        {
            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Blend", 1.35f);
            InMotion = true;
        }

        //RUNNIGN FORWARD LEFT
        public void RunningForwardLeft()
        {
            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Blend", 2f);
            InMotion = true;
        }

        //RUNNING BACKWARD
        public void RunningBackward()
        {
            Debug.Log("HERE CHARACTER RUNS");
            playerMotion.SetFloat("Blend", 1.1f);
            InMotion = true;
        }

        //JUMP
        public void Jump()
        {
            Debug.Log("HERE CHARACTER JUMPS");
            InMotion = true;
            playerMotion.SetBool("IsJump", true);
        }

        //DEATH
        public void Death()
        {
            IsActive = true;
            IsDead = true;
        }
     */
    #endregion

    // FOR 2D BLEND TREE
    #region ANIMATIONS METHODS ARE HERE

    //IDLE
    public void Idle()
        {
            if (Direction > 0f)
            {
                Direction -= animAdjNumber;
                Debug.Log(Direction);
            }
            if (Speed > 0f)
            {
                Speed -= animAdjNumber;
                Debug.Log(Speed);
            }
            if (Direction < 0f)
            {
                Direction += animAdjNumber;
                Debug.Log(Direction);
            }
            if (Speed < 0f)
            {
                Speed += animAdjNumber;
                Debug.Log(Speed);
            }
            
            IsActive = true;
            IsDead = false;
            Debug.Log("IDLE STARTS HERE");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            playerMotion.SetBool("IsJump", false);

        }

        //WALKING FORWARD
        public void WalkingForward() {

            if (Direction < 0.5f)
            { Direction += animAdjNumber; Debug.Log(Direction); }
            if (Direction > 0.5f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }

            //Set X Axis back to 0
            if (Speed < 0)
            { Speed += animAdjNumber; Debug.Log(Speed); }
            if (Speed > 0)
            { Speed -= animAdjNumber; Debug.Log(Speed); }


            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //WALKING LEFT
        public void WalkingLeft()
        {

            if (Speed > -0.5f)
            { Speed -= animAdjNumber; Debug.Log(Speed); }
            if (Speed < -0.5f)
            { Speed += animAdjNumber; Debug.Log(Speed); }

            //Set Y Axis back to 0
            if (Direction > 0f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }
            if (Direction < 0f)
            { Direction += animAdjNumber; Debug.Log(Direction); }

            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //WALKING RIGHT
        public void WalkingRight()
        {
            if (Speed < 0.5f)
            { Speed += animAdjNumber; Debug.Log(Speed); }
            if (Speed > 0.5f)
            { Speed -= animAdjNumber; Debug.Log(Speed); }

            //Set Y Axis back to 0
            if (Direction > 0f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }
            if (Direction < 0f)
            { Direction += animAdjNumber; Debug.Log(Direction); }

            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }


        //WALKING BACKWARD
        public void WalkingBackward()
        {
            if (Direction > -0.5f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }
            if (Direction < -0.5f )
            { Direction += animAdjNumber; Debug.Log(Direction); }


            //Set X Axis back to 0
            if (Speed < 0)
            { Speed += animAdjNumber; Debug.Log(Speed); }
            if (Speed > 0)
            { Speed -= animAdjNumber; Debug.Log(Speed); }

            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //RUNNING FORWARD
        public void RunningForward()
        {
            if (Direction < 1f)
            { Direction += animAdjNumber; Debug.Log(Direction); }
            if (Direction > 0f)
            { Direction += animAdjNumber; Debug.Log(Direction); }


            //Set X Axis back to 0
            if (Speed < 0)
            { Speed += animAdjNumber; Debug.Log(Speed); }
            if (Speed > 0)
            { Speed -= animAdjNumber; Debug.Log(Speed); }

            Debug.Log("HERE CHARACTER RUNS");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //RUNNING LEFT
        public void RunningLeft()
        {
            if (Speed > -1f)
            { Speed -= animAdjNumber; Debug.Log(Speed); }
            if (Speed < 0f)
            { Speed -= animAdjNumber; Debug.Log(Speed); }

            //Set Y Axis back to 0
            if (Direction > 0f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }
            if (Direction < 0f)
            { Direction += animAdjNumber; Debug.Log(Direction); }

            Debug.Log("HERE CHARACTER RUNS");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //RUNNING RIGHT
        public void RunningRight()
        {
            if (Speed < 1f)
            { Speed += animAdjNumber; Debug.Log(Speed); }
            if (Speed > 0f)
            { Speed += animAdjNumber; Debug.Log(Speed); }

            //Set Y Axis back to 0
            if (Direction > 0f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }
            if (Direction < 0f)
            { Direction += animAdjNumber; Debug.Log(Direction); }

            Debug.Log("HERE CHARACTER RUNS");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //WALKING FORWARD LEFT
        public void WalkingForwardLeft()
        {
            if (Speed > -0.5f)
            {
                Speed -= animAdjNumber; Debug.Log(Speed);
            }
            else if (Speed > -0.499f)
            { Speed = -0.5f; Direction = 0.5f; Debug.Log(Speed); }

            if (Direction < 0.5f)
            { Direction += animAdjNumber; Debug.Log(Direction); }

            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //WALKING FORWARD RIGHT
        public void WalkingForwardRight()
        {
            if (Speed < 0.5f)
            { Speed += animAdjNumber; Debug.Log(Speed); }


            if (Direction < 0.5f)
            { Direction += animAdjNumber; Debug.Log(Direction); }

            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //RUNNING FORWARD RIGHT
        public void RunningForwardRight()
        {
            if (Speed < 1f)
            { Speed += animAdjNumber; Debug.Log(Speed); }

            if (Speed > 0f)
            { Speed += animAdjNumber; Debug.Log(Speed); }

            if (Direction < 1f)
            { Direction += animAdjNumber; Debug.Log(Direction); }

            else if (Speed > -0.999f)
            { Speed = -1f; Direction = 1f; Debug.Log(Speed); }

            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //RUNNIGN FORWARD LEFT
        public void RunningForwardLeft()
        {
            if (Speed > -1f)
            { Speed -= animAdjNumber; Debug.Log(Speed); }
            if (Speed < 0f)
            { Speed -= animAdjNumber; Debug.Log(Speed); }
            if (Direction < 1f)
            { Direction += animAdjNumber; Debug.Log(Direction); }
            if (Direction > 0f)
            { Direction += animAdjNumber; Debug.Log(Direction); }

            else if (Speed > -0.999f)
            { Speed = -1;  Direction = 1; }


            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //RUNNING BACKWARD
        public void RunningBackward()
        {
            if (Direction > -1f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }
            if (Direction < 0f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }

            //Set X Axis back to 0
            if (Speed < 0)
            { Speed += animAdjNumber; Debug.Log(Speed); }
            if (Speed > 0)
            { Speed -= animAdjNumber; Debug.Log(Speed); }

            Debug.Log("HERE CHARACTER RUNS");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //WALKING BACKWARD RIGHT
        public void WalkingBackwardRight()
        {
            if (Speed < 0.5f)
            { Speed += animAdjNumber; Debug.Log(Speed); }
            if (Direction > -0.5f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }

            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //WALKING BACKWARD LEFT
        public void WalkingBackwardLeft()
        {
            if (Speed > -0.5f)
            { Speed -= animAdjNumber; Debug.Log(Speed); }
            if (Direction > -0.5f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }

            Debug.Log("WALKING STARTS HERE");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }


        //RUNNING BACKWARD RIGHT
        public void RunningBackwardRight()
        {

            if (Speed < 1f)
            { Speed += animAdjNumber; Debug.Log(Speed); }
            if (Speed > 0f)
            { Speed += animAdjNumber; Debug.Log(Speed); }
            if (Direction > -1f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }
            if (Direction < 0f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }
            else if (Speed > 0.999f)
            { Speed = 1; Direction = -1; }

            Debug.Log("HERE CHARACTER RUNS");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //RUNNING BACKWARD LEFT
        public void RunningBackwardLeft()
        {
            if (Speed > -1f)
            { Speed -= animAdjNumber; Debug.Log(Speed); }
            if (Speed < 0f)
            { Speed -= animAdjNumber; Debug.Log(Speed); }
            if (Direction > -1f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }
            if (Direction < 0f)
            { Direction -= animAdjNumber; Debug.Log(Direction); }
            else if (Direction < -0.999f)
            { Speed = -1; Direction = -1; }

            Debug.Log("HERE CHARACTER RUNS");
            playerMotion.SetFloat("Speed", Speed);
            playerMotion.SetFloat("Direction", Direction);
            InMotion = true;
        }

        //SHOOTING
        public void Shooting()
        {

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                Debug.Log("HERE CHARACTER SHOOTING");
                InMotion = true;
                playerMotion.SetBool("IsShot", true);
            }
            Debug.Log("HERE CHARACTER SHOOTING");
            playerMotion.SetBool("IsShot", true);
            
        }


        //JUMP
        public void Jump()
        {
            Debug.Log("HERE CHARACTER JUMPS");
            InMotion = true;
            playerMotion.SetBool("IsJump", true);
        }

        //DEATH
        public void Death()
        {
            IsActive = true;
            IsDead = true;
            playerMotion.SetBool("IsDead", true);
        }


    #endregion

    /*
    void Update() {

        if (!isLocalPlayer) { return; }

        InMotion = false;
        playerMotion.SetBool("IsShot", false);


        
        // JUST FOR TESTING 

        //Walking and Run Forward -----> 
        if (Input.GetKey(KeyCode.W))
        {
            WalkingForward();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                RunningForward();
            }
        }

        //Walking and Running Backward -----> 
        if (Input.GetKey(KeyCode.S))
        {
            WalkingBackward();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                RunningBackward();
            }
        } 
        
        //Walking and Running Left
        if (Input.GetKey(KeyCode.A))
        {
            WalkingLeft();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                RunningLeft();
            }
        }
        //Walking and Running Right
        if (Input.GetKey(KeyCode.D))
        {
            WalkingRight();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                RunningRight();
            }
        }

        //Walking Forward Right
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            WalkingForwardRight();
        }
        //Walking Forward Left
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            WalkingForwardLeft();
        }
        //Running Forward Right
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            RunningForwardRight();
        }
        //Running Forward Left
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            RunningForwardLeft();
        }
        //Walking Backward Right
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            WalkingBackwardRight();
        }
        //Walking Backward Left
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            WalkingBackwardLeft();
        }

        //Running Backward Right
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            RunningBackwardRight();
        }
        //Running Backward Left
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            RunningBackwardLeft();
        }

        //Shooting
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shooting();
        }


        //Jumping -----> 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Death();
        }


        if (IsActive && !IsDead && !InMotion)
        {
            Debug.Log("IDLE TURNING BACK");
            Idle();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerMotion.runtimeAnimatorController = controllerGun2D;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerMotion.runtimeAnimatorController = controllerRifle2D;
        }

    }
    */
}
