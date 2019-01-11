using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterStateManager : NetworkBehaviour
{


    public bool IsActive = false;
    public bool IsDead = false;
    public bool InMotion = false;


    public Animator playerMotion;

    public RuntimeAnimatorController controllerGun2D;
    public RuntimeAnimatorController controllerRifle2D;

    public GameObject laserRifle;
    public GameObject sniperRifle;
    public GameObject granadeLauncher;
    public GameObject gun;

    private float Direction = 0.0f;
    private float Speed = 0.0f;
    private float animAdjNumber = 0.3f; // number which effects how fast switching between states

    private void Awake()
    {
        InitializeMe();
        

        laserRifle = GameManager.Instance().gunHolder.transform.Find("LaserRifle").gameObject;
        sniperRifle = GameManager.Instance().gunHolder.transform.Find("SniperRifle").gameObject;
        granadeLauncher = GameManager.Instance().gunHolder.transform.Find("GranadeLauncher").gameObject;
        gun = GameManager.Instance().gunHolder.transform.Find("Gun").gameObject;
        //
        laserRifle.SetActive(false);
        sniperRifle.SetActive(false);
        granadeLauncher.SetActive(false);
        gun.SetActive(false);
    }


    public void InitializeMe()
    {
        playerMotion.SetFloat("Speed", 0);
        playerMotion.SetFloat("Direction", 0);
        playerMotion.SetBool("IsJump", false);
        playerMotion.SetBool("IsDead", false);
        playerMotion.SetBool("IsSniperAim", false);
        playerMotion.SetBool("IsShot", false);
        IsActive = true;
        IsDead = false;
        InMotion = false;


}


    // FOR 2D BLEND TREE
    #region ANIMATIONS METHODS ARE HERE

    //IDLE
    public void Idle()
    {
        if (Direction > 0f)
        {
            Direction -= animAdjNumber;
            // Debug.Log(Direction);
        }
        if (Speed > 0f)
        {
            Speed -= animAdjNumber;
            // Debug.Log(Speed);
        }
        if (Direction < 0f)
        {
            Direction += animAdjNumber;
            // Debug.Log(Direction);
        }
        if (Speed < 0f)
        {
            Speed += animAdjNumber;
            // Debug.Log(Speed);
        }

        IsActive = true;
        IsDead = false;
        // Debug.Log("IDLE STARTS HERE");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        playerMotion.SetBool("IsJump", false);

    }

    //WALKING FORWARD
    public void WalkingForward()
    {

        if (Direction < 0.5f)
        { Direction += animAdjNumber; }
        if (Direction > 0.5f)
        { Direction -= animAdjNumber; }

        //Set X Axis back to 0
        if (Speed < 0)
        { Speed += animAdjNumber; }
        if (Speed > 0)
        { Speed -= animAdjNumber; }


        // Debug.Log("WALKING STARTS HERE");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //WALKING LEFT
    public void WalkingLeft()
    {

        if (Speed > -0.5f)
        { Speed -= animAdjNumber; }
        if (Speed < -0.5f)
        { Speed += animAdjNumber; }

        //Set Y Axis back to 0
        if (Direction > 0f)
        { Direction -= animAdjNumber; }
        if (Direction < 0f)
        { Direction += animAdjNumber; }

        // Debug.Log("WALKING STARTS HERE");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //WALKING RIGHT
    public void WalkingRight()
    {
        if (Speed < 0.5f)
        { Speed += animAdjNumber; }
        if (Speed > 0.5f)
        { Speed -= animAdjNumber; }

        //Set Y Axis back to 0
        if (Direction > 0f)
        { Direction -= animAdjNumber; }
        if (Direction < 0f)
        { Direction += animAdjNumber; }

        // Debug.Log("WALKING STARTS HERE");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }


    //WALKING BACKWARD
    public void WalkingBackward()
    {
        if (Direction > -0.5f)
        { Direction -= animAdjNumber; }
        if (Direction < -0.5f)
        { Direction += animAdjNumber; }


        //Set X Axis back to 0
        if (Speed < 0)
        { Speed += animAdjNumber; }
        if (Speed > 0)
        { Speed -= animAdjNumber; }

        // Debug.Log("WALKING STARTS HERE");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //RUNNING FORWARD
    public void RunningForward()
    {
        if (Direction < 1f)
        { Direction += animAdjNumber; }
        if (Direction > 0f)
        { Direction += animAdjNumber; }


        //Set X Axis back to 0
        if (Speed < 0)
        { Speed += animAdjNumber; }
        if (Speed > 0)
        { Speed -= animAdjNumber; }

        // Debug.Log("HERE CHARACTER RUNS");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //RUNNING LEFT
    public void RunningLeft()
    {
        if (Speed > -1f)
        { Speed -= animAdjNumber; }
        if (Speed < 0f)
        { Speed -= animAdjNumber; }

        //Set Y Axis back to 0
        if (Direction > 0f)
        { Direction -= animAdjNumber; }
        if (Direction < 0f)
        { Direction += animAdjNumber; }

        // Debug.Log("HERE CHARACTER RUNS");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //RUNNING RIGHT
    public void RunningRight()
    {
        if (Speed < 1f)
        { Speed += animAdjNumber; }
        if (Speed > 0f)
        { Speed += animAdjNumber; }

        //Set Y Axis back to 0
        if (Direction > 0f)
        { Direction -= animAdjNumber; }
        if (Direction < 0f)
        { Direction += animAdjNumber; }

        // Debug.Log("HERE CHARACTER RUNS");
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
        { Speed = -0.5f; Direction = 0.5f; }

        if (Direction < 0.5f)
        { Direction += animAdjNumber; }

        // Debug.Log("WALKING STARTS HERE");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //WALKING FORWARD RIGHT
    public void WalkingForwardRight()
    {
        if (Speed < 0.5f)
        { Speed += animAdjNumber; }


        if (Direction < 0.5f)
        { Direction += animAdjNumber; }

        // Debug.Log("WALKING STARTS HERE");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //RUNNING FORWARD RIGHT
    public void RunningForwardRight()
    {
        if (Speed < 1f)
        { Speed += animAdjNumber; }

        if (Speed > 0f)
        { Speed += animAdjNumber; }

        if (Direction < 1f)
        { Direction += animAdjNumber; }

        else if (Speed > -0.999f)
        { Speed = -1f; Direction = 1f; }

        // Debug.Log("WALKING STARTS HERE");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //RUNNIGN FORWARD LEFT
    public void RunningForwardLeft()
    {
        if (Speed > -1f)
        { Speed -= animAdjNumber; }
        if (Speed < 0f)
        { Speed -= animAdjNumber; }
        if (Direction < 1f)
        { Direction += animAdjNumber; }
        if (Direction > 0f)
        { Direction += animAdjNumber; }

        else if (Speed > -0.999f)
        { Speed = -1; Direction = 1; }


        // Debug.Log("WALKING STARTS HERE");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //RUNNING BACKWARD
    public void RunningBackward()
    {
        if (Direction > -1f)
        { Direction -= animAdjNumber; }
        if (Direction < 0f)
        { Direction -= animAdjNumber; }

        //Set X Axis back to 0
        if (Speed < 0)
        { Speed += animAdjNumber; }
        if (Speed > 0)
        { Speed -= animAdjNumber; }

        // Debug.Log("HERE CHARACTER RUNS");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //WALKING BACKWARD RIGHT
    public void WalkingBackwardRight()
    {
        if (Speed < 0.5f)
        { Speed += animAdjNumber; }
        if (Direction > -0.5f)
        { Direction -= animAdjNumber; }

        // Debug.Log("WALKING STARTS HERE");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //WALKING BACKWARD LEFT
    public void WalkingBackwardLeft()
    {
        if (Speed > -0.5f)
        { Speed -= animAdjNumber; }
        if (Direction > -0.5f)
        { Direction -= animAdjNumber; }

        // Debug.Log("WALKING STARTS HERE");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }


    //RUNNING BACKWARD RIGHT
    public void RunningBackwardRight()
    {

        if (Speed < 1f)
        { Speed += animAdjNumber; }
        if (Speed > 0f)
        { Speed += animAdjNumber; }
        if (Direction > -1f)
        { Direction -= animAdjNumber; }
        if (Direction < 0f)
        { Direction -= animAdjNumber; }
        else if (Speed > 0.999f)
        { Speed = 1; Direction = -1; }

        // Debug.Log("HERE CHARACTER RUNS");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //RUNNING BACKWARD LEFT
    public void RunningBackwardLeft()
    {
        if (Speed > -1f)
        { Speed -= animAdjNumber; }
        if (Speed < 0f)
        { Speed -= animAdjNumber; }
        if (Direction > -1f)
        { Direction -= animAdjNumber; }
        if (Direction < 0f)
        { Direction -= animAdjNumber; }
        else if (Direction < -0.999f)
        { Speed = -1; Direction = -1; }

        // Debug.Log("HERE CHARACTER RUNS");
        playerMotion.SetFloat("Speed", Speed);
        playerMotion.SetFloat("Direction", Direction);
        InMotion = true;
    }

    //SHOOTING
    public void Shooting()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            // Debug.Log("HERE CHARACTER SHOOTING");
            InMotion = true;
            playerMotion.SetBool("IsShot", true);
        }
        // Debug.Log("HERE CHARACTER SHOOTING");
        playerMotion.SetBool("IsShot", true);

    }


    //JUMP
    public void Jump()
    {
        // Debug.Log("HERE CHARACTER JUMPS");
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


    //Character takes LaserRifle in hands
    public void TakeLaserRifle()
    {
        laserRifle.SetActive(true);
        sniperRifle.SetActive(false);
        granadeLauncher.SetActive(false);
        gun.SetActive(false);
        TurnToRifleController();
    }

    //Character takes SniperRifle in hands
    public void TakeSniperRifle()
    {
        laserRifle.SetActive(false);
        sniperRifle.SetActive(true);
        granadeLauncher.SetActive(false);
        gun.SetActive(false);
        TurnToRifleController();
    }

    //Character takes GranadeLauncher in hands
    public void TakeGranadeLauncher()
    {
        laserRifle.SetActive(false);
        sniperRifle.SetActive(false);
        granadeLauncher.SetActive(true);
        gun.SetActive(false);
        TurnToRifleController();
    }

    //Character takes GranadeLauncher in hands
    public void TakeGun()
    {
        laserRifle.SetActive(false);
        sniperRifle.SetActive(false);
        granadeLauncher.SetActive(false);
        gun.SetActive(true);
        TurnToGunController();
    }

    //SniperAiming 
    public void SniperAim()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            // Debug.Log("HERE CHARACTER AIMING");
            InMotion = true;
            playerMotion.SetBool("IsSniperAim", true);
        }
        // Debug.Log("HERE CHARACTER AIMING");
        playerMotion.SetBool("IsSniperAim", true);



        if (IsActive && !IsDead && !InMotion)
        {
            // Debug.Log("IDLE TURNING BACK");
            Idle();
        }

    }

    //Switch controller to Rifle controller
    public void TurnToRifleController()
    {
        playerMotion.runtimeAnimatorController = controllerRifle2D;
    }
    //Switch controller to Gun controller
    public void TurnToGunController()
    {
        playerMotion.runtimeAnimatorController = controllerGun2D;
    }

}
