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
    public Ability ability;

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


    public Color screenFlashColor = Color.white;
    public float screenFlashTimer = 10.0f; // *** A high number so it doesn't flash after spawning.

    private void OnGUI()
    {
        if (isLocalPlayer)
        {
            if (screenFlashTimer < 1.0f)
            {
                float jist = Helper.Longevity(screenFlashTimer, 0, 0.5f);
                //
                GUI.color = new Color(screenFlashColor.r, screenFlashColor.g, screenFlashColor.b, 0.3f * Mathf.Sin(Mathf.PI * jist));
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Graphics.Instance().screenTex);
            }
        }
    }


    public override void Awake()
    {
        base.Awake();
        //
        input = new PlayerInputter();

        
        controller = GetComponent<FPSController2>();
        if (controller == null) { controller = gameObject.AddComponent<FPSController2>(); }


        pch = GetComponent<PlayerCommandHolder>();
    }

    public override void Start()
    {
        base.Start();
        //
    }


    public void SetThirdPerson(bool state)
    {
        if (!isLocalPlayer) { return; }


        // *** Hide the player model if you're in first person view!
        if (state)
        {
            Cam.SetActiveCamera(camPerspectiveThird);
            playerModel.gameObject.SetActive(true);
        }
        else
        {
            Cam.SetActiveCamera(camPerspectiveFirst);
            playerModel.gameObject.SetActive(false);
        }
    }


    public override void Act()
    {
        if (!isLocalPlayer) { return; }


        screenFlashTimer += Time.deltaTime;
        lifeTimer += Time.deltaTime;


        base.Act();
        //
        input.Read();



        if (stateManager.IsDead)
        {
            if (input.jump && !input.lastJump)
            {
                Respawn();
            }
            return;
        }



        if (input.fire2 && !input.lastFire2)
        {
            healthStatus.AlterHealth(-10);
            OnTakeDamage(-10);


            // Helper.ClearMessages();
            //
            // pch.CmdCreateBulletTrail(transform.position, transform.position + LookVector * 100, 1.0f, Color.blue);
            // pch.CmdSpawn("Bullet", NetworkID, LookVector);
        }



        // *** Weapon stuff.
        weapon.Check();
        //
        weapon.Interact();


        // *** Ability stuff.
        ability.Run();
        if (ability.IsActivated) { ability.RunWhileActive(); }
        else { ability.RunWhileActive(); }
        ability.LateRun();
        //
        ability.Interact(input);
    }


    public override void LateAct()
    {
        followReeling = Mathf.Lerp(followReeling, recoilReeling, 0.1f); // ??? <-- This number is completely made up... I have no real reason for using it...
        //
        recoilReeling = Mathf.Max(0, recoilReeling - 2.5f * Time.deltaTime); // *** Takes roughly a 3rd of a second to recover from maximum recoil reeling.
        //
        // ??? <-- TODO: Make this value actually adjust the character's POV...
    }



    public override void InitializeMe()
    {
        base.InitializeMe();
        //
        controller.enabled = true;


        // ??? <-- Debugging code.
        weapon = new Weapon(new Rifle(), this); // Pistol
    }
    public override void Respawn()
    {
        base.Respawn();
    }


    internal float lifeTimer = 0.0f;
    public override void Die()
    {
        base.Die();
        //
        controller.enabled = false;
        lifeTimer = 0.0f;

        SetThirdPerson(true);
    }



    #region Recoil stuff.
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
    #endregion



    public override void OnSpawned(Vector3 lookVector)
    {
        base.OnSpawned(lookVector);
        //
        SetThirdPerson(false);
    }

    public override void OnTakeDamage(float amount)
    {
        base.OnTakeDamage(amount);
        //
        if (amount < 0)
        {
            screenFlashColor = Color.red;
            screenFlashTimer = 0.0f;
        }
    }

}
