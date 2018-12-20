using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Represents a character that is controlled by a player.
/// Its primary difference is that it can receive and respond to inputs.
/// </summary>
public class ControllableCharacter : Character {

    public BaseInputter input;
    public Weapon weapon;

    internal FPSController2 controller;

    public Cam camPerspectiveFirst;
    public Cam camPerspectiveThird;


    /// <summary>
    /// Contains all the network functionality that players use.
    /// </summary>
    internal PlayerCommandHolder pch;


    /// <summary>
    /// This override uses a "special" Look Vector if an FPSController exists.
    /// </summary>
    public override Vector3 LookVector
    {
        // ??? <-- This needs to be implemented INTO the controller itself.
        get
        {
            if (controller == null) { return base.LookVector; }
            Vector3 look = controller.LookVector; // con.attachedCamera.transform.forward
            //
            look.x -= 90 * followReeling;
            look.x = Mathf.Max(-90, look.x);
            //
            return Quaternion.Euler(look) * Vector3.forward;
        }
    }


    public override void Awake()
    {
        base.Awake();
        //
        input = new PlayerInputter();

        // ??? <-- Debugging code.
        weapon = new Weapon(new Rifle(), this); // Pistol


        controller = GetComponent<FPSController2>();
        if (controller == null) { controller = gameObject.AddComponent<FPSController2>(); }


        pch = GetComponent<PlayerCommandHolder>();
    }

    public override void Start()
    {
        base.Start();
        //
        if (isLocalPlayer)
        {
            if (false && camPerspectiveThird != null)
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


        

        // *** Hide the player model if you're in first person view!
        if (isLocalPlayer && Cam.currentCam == camPerspectiveFirst)
        {
            playerModel.gameObject.SetActive(false);
        }
    }


    public override void Act()
    {
        if (!isLocalPlayer) { return; }


        base.Act();
        //
        input.Read();

        

        if (input.fire2 && !input.lastFire2)
        {
            // Helper.ClearMessages();
            //
            pch.CmdCreateBulletTrail(transform.position, transform.position + LookVector * 100, 1.0f, Color.blue);
            // pch.CmdSpawn("Bullet", NetworkID, LookVector);
        }



        // *** Weapon stuff.
        weapon.Check();
        //
        weapon.Interact();
    }


    public override void LateAct()
    {
        followReeling = Mathf.Lerp(followReeling, recoilReeling, 0.1f); // ??? <-- This number is completely made up... I have no real reason for using it...
        //
        recoilReeling = Mathf.Max(0, recoilReeling - 2.5f * Time.deltaTime); // *** Takes roughly a 3rd of a second to recover from maximum recoil reeling.
        //
        // ??? <-- TODO: Make this value actually adjust the character's POV...
    }


    public float RecoilValue { get { return Mathf.Clamp01(followReeling); } }

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
