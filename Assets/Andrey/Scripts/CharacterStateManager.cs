using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour {


    public bool IsActive = false;
    public bool IsDead = false;

    bool IsIdle = false;
    bool IsWalking = false;
    bool IsRunning = false;
    bool IsJumping = false;
    bool IsGhost = false;

    public Animator playerMotion;

    //Idle
    public void Idle()
    {
        IsActive = true;
        IsIdle = true;
        IsWalking = false;
        IsRunning = false;
        IsJumping = false;
        Debug.Log("IDLE STARTS HERE");
        playerMotion.SetBool("IsWalking", false);
    }

    //Walking
    public void Walking() {

        
        IsIdle = false;
        IsWalking = true;
        IsRunning = false;
        IsJumping = false;
        Debug.Log("WALKING STARTS HERE");
        playerMotion.SetBool("IsWalking", true);
    }

    //Sprint
    public void Running()
    {
        IsIdle = false;
        IsWalking = false;
        IsRunning = true;
        IsJumping = false;
        Debug.Log("HERE CHARACTER RUNS");
    }

    //Jump
    public void Jump()
    {
        IsIdle = false;
        IsWalking = false;
        IsRunning = false;
        IsJumping = true;
        Debug.Log("HERE CHARACTER JUMPS");
    }



    void Update() {


        //Walking and Run -----> 
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Running();
            }
            else { Walking(); }

        }
        //Jumping -----> 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (IsActive && !IsWalking && !IsRunning && !IsJumping)
        {
            Idle();
        }

        IsIdle = false;
        IsWalking = false;
        IsRunning = false;
        IsJumping = false;
    }
}
