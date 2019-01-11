using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : Ability {

    public GameObject hook;
    public GameObject hookHolder;

    //speed of hook when fired
    public float hookTravelSpeed;

    //player speed when hooked to object
    public float playerTravelSpeed;

    public static bool fired;
    public bool hooked;

    public GameObject hookedObj;

    //max distance hook can travel
    public float maxDistance;

    //dynamic float that will be detected before maxDistance is reached
    private float currentDistance;

    private bool grounded;

    public GrapplingHook(ControllableCharacter cc) : base(cc)
    {

    }

    public override void Init()
    {
        base.Init();
        //
        hook = RickController.Grapplinghook;
        hookHolder = RickController.GrappleHolder;

        activationMode = ActivationMode.Lingering;
        activatedDuration = 10.0f;

    }

    public override void Run()
    {
        base.Run();
        //
            Debug.Log("TESTING BRUH");

            Debug.Log(hook);
            Debug.Log(hookHolder);

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
        Debug.Log("Fucking ability working bruh");
        if (Input.GetMouseButtonDown(2) && fired == false)
            fired = true;

        if (fired)
        {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        if (fired == true && hooked == false)
        {
            // moved hook towards aimed point
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);

            //calculate the distance between the hook and player so hook doesnt travel to far
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            //return hook
            if (currentDistance >= maxDistance)
                ReturnHook();
        }

        //start moving player to hooked location
        if (hooked == true && fired == true)
        {
            hook.transform.parent = hookedObj.transform;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            //disable player rigidbody
            this.GetComponent<Rigidbody>().useGravity = false;

            if (distanceToHook < 1.5)
            {
                if (grounded == false)
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 10f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 15f);
                }

                StartCoroutine("Climb");
            }
        }
        else
        {
            hook.transform.parent = hookHolder.transform;

            //enable gravity when not hooked to anything
            this.GetComponent<Rigidbody>().useGravity = true;
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
        hooked = false;

        //disable line renderer for rope
        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.SetVertexCount(0);
    }

    private void CheckIfGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;
        } else {
            grounded = false;
        }
    }
}