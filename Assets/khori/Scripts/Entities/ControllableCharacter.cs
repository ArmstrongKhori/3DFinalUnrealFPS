using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ControllableCharacter : Character {

    public BaseInputter input;
    public Weapon weapon;

    internal FPSController2 controller;

    public Cam camPerspectiveFirst;
    public Cam camPerspectiveThird;


    internal PlayerCommandHolder pch;


    public override Vector3 LookVector
    {
        // ??? <-- This needs to be implemented INTO the controller itself.
        get
        {
            return controller == null ? base.LookVector : Quaternion.Euler(controller.LookVector) * Vector3.forward; // con.attachedCamera.transform.forward
        }
    }


    public override void Awake()
    {
        base.Awake();
        //
        input = new PlayerInputter();

        // ??? <-- Debugging code.
        weapon = new Weapon(new Pistol(), this);


        controller = GetComponent<FPSController2>();
        if (controller == null) { controller = gameObject.AddComponent<FPSController2>(); }
    }

    public override void Start()
    {
        base.Start();
        //
        if (isLocalPlayer)
        {
            if (camPerspectiveThird != null)
            {
                Cam.SetActiveCamera(camPerspectiveThird);
            }
            else
            {
                if (camPerspectiveFirst != null)
                {
                    Cam.SetActiveCamera(camPerspectiveFirst);
                }
            }
        }


        pch = GetComponent<PlayerCommandHolder>();
    }


    public override void Act()
    {
        if (!isLocalPlayer) { return; }


        base.Act();
        //
        input.Read();

        

        if (input.fire2 && !input.lastFire2)
        {
            // BulletTrail.Create(transform.position, transform.position + LookVector*100, 1.0f, Color.blue);


            // Helper.ClearMessages();
            //
            pch.CmdCreateBulletTrail(transform.position, transform.position + LookVector * 100, 1.0f, Color.blue);
            // pch.CmdSpawn("Bullet", NetworkID, LookVector);


            // Network_Interact(InteractVerbs.TestBlock, NetworkID, new InteractData(new Vector3(transform.position.x, transform.position.y, transform.position.z)));

            /*
            foreach (Character c in FindObjectsOfType<Character>())
            {
                if (c.NetworkID != NetworkID)
                {
                    Network_Interact(InteractVerbs.Damage, c.NetworkID, new InteractData(5));
                }
            }
            */
        }
        

        /*
        Vector3 moveDirection = Vector3.zero;
        //
        moveDirection += new Vector3(10 * 0, 0, 10 * input.moving); // input.strafing

        transform.Rotate(new Vector3(0, 1 * input.strafing, 0));
        //
        rb.velocity = transform.TransformDirection(moveDirection); //  * Time.deltaTime
        */


        // *** Weapon stuff.
        weapon.Check();
        //
        weapon.Interact();
    }


    public virtual void LateAct()
    {
        followReeling = Mathf.Lerp(followReeling, recoilReeling, 0.1f); // ??? <-- This number is completely made up... I have no real reason for using it...
        //
        recoilReeling -= 2.5f * Time.deltaTime; // *** Takes roughly a 3rd of a second to recover from maximum recoil reeling.
        //
        // ??? <-- TODO: Make this value actually adjust the character's POV...
    }


    /// <summary>
    /// This value "adjusts" your view's pitch.
    /// </summary>
    private float recoilReeling = 0.0f;
    /// <summary>
    /// This value "follows" the current reeling value.
    /// This allows a more "realistic" swinging of the view when under high recoil.
    /// </summary>
    private float followReeling = 0.0f;
    public void ApplyRecoil(float amount)
    {
        recoilReeling += amount;
    }

}
