using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : Ability {

    public GameObject hook;
    public GameObject hookHolder;

    //speed of hook when fired
    public float hookTravelSpeed = 15.0f;

    //player speed when hooked to object
    public float playerTravelSpeed = 12.0f;

    public static bool fired;

    //max distance hook can travel
    public float maxDistance = 15.0f;

    //dynamic float that will be detected before maxDistance is reached
    private float currentDistance;

    private bool grounded;

    private HookDetector HookCheck;

    public GrapplingHook(ControllableCharacter cc) : base(cc)
    {

    }

    public override void Init()
    {
        base.Init();
        //
        hook = GameObject.Find("Camera/Hook Holder/Hook");
        hookHolder = GameObject.Find("Camera/Hook Holder");

        HookCheck = hook.GetComponent<HookDetector>();

        activationMode = ActivationMode.Lingering;
        activatedDuration = 10.0f;

    }

    public override void Run()
    {
        base.Run();
        //
    }
    public override void LateRun()
    {
        base.LateRun();
        //

    }
    public override void RunWhileActive()
    {
        
        base.RunWhileActive();
        //
        // firing hook
        Debug.Log(hookHolder);
        if (Input.GetMouseButtonDown(2) && fired == false)
            fired = true;

        if (fired)
        {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.positionCount = 2;
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        if (fired == true && HookCheck.hooked == false)
        {
            // moved hook towards aimed point
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);

            //calculate the distance between the hook and player so hook doesnt travel to far
            currentDistance = Vector3.Distance(owner.transform.position, hook.transform.position);

            //return hook
            if (currentDistance >= maxDistance)
                ReturnHook();
        }

        //start moving player to hooked location
        if (HookCheck.hooked == true && fired == true)
        {
            RickController.NoJump = true;
            hook.transform.parent = HookCheck.HookedObject.transform;
            owner.transform.position = Vector3.MoveTowards(owner.transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHook = Vector3.Distance(owner.transform.position, hook.transform.position);

            //disable player rigidbody
            owner.GetComponent<Rigidbody>().useGravity = false;

            if (distanceToHook < 1.5)
            {
                ReturnHook();
                if (grounded == false)
                {
                    owner.transform.Translate(Vector3.forward * Time.deltaTime * 10f);
                    owner.transform.Translate(Vector3.up * Time.deltaTime * 15f);
                }

                //StartCoroutine("Climb");
            }
        }
        else
        {
            hook.transform.parent = hookHolder.transform;

            //enable gravity when not hooked to anything
            owner.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public override void RunWhileInactive()
    {
        base.RunWhileInactive();
        //

    }
    public override void OnKill()
    {
        base.OnKill();
        //

    }
    public override void OnFiveKills()
    {
        base.OnFiveKills();
        //

    }
    public override void OnTriggered()
    {
        base.OnTriggered();
        //

    }
    public override void OnActivate()
    {
        base.OnActivate();
        //

    }
    public override void OnDeactivate()
    {
        base.OnDeactivate();
        //
        RickController.NoJump = false;
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
    }

    private void ReturnHook()
    {
        //resets the hooks pos/rot to the hook holder
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        HookCheck.hooked = false;

        //disable line renderer for rope
        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.positionCount = 0;
    }

    
    private void CheckIfGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(owner.transform.position, dir, out hit, distance))
        {
            grounded = true;
        } else {
            grounded = false;
        }
    }
}