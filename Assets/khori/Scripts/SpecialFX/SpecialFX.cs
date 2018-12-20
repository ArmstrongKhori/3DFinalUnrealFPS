using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This class utilizes a combination of particle effects, trail renderers, etc... To produce special effects.
/// </summary>
public class SpecialFX : NetworkBehaviour {


    internal bool hasBegun = false;
    internal bool hasEnded = false;
    internal float runTime = 0.0f;
    internal float endTime = 0.0f;

    /// <summary>
    /// Once it has been running for this much time, it will automatically call the "End" function.
    /// </summary>
    internal float expiresDate = 1.0f;


    /// <summary>
    /// This tells the object to "begin" doing whatever it does.
    /// This allows you to "preload" the object and make use of timing.
    /// NOTE: By default, the object immediately begins when created.
    /// </summary>
    public void Begin()
    {
        if (!hasEnded)
        {
            if (!hasBegun)
            {
                hasBegun = true;
                //
                OnBegin();
            }
        }
    }
    /// <summary>
    /// Rather than immediately despawning, this allows the object time to "expire" its particles effects and whatnot.
    /// In other words, this "gently" asks the object to stop creating things and to consider disappearing.
    /// </summary>
    public void End()
    {
        if (hasBegun)
        {
            if (!hasEnded)
            {
                hasEnded = true;
                //
                OnEnded();
            }
        }
    }


    public virtual void OnNetworkCreation()
    {
        // CmdSpawnMe();
    }


    [Command]
    public void CmdSpawnMe()
    {
        NetworkServer.Spawn(this.gameObject);
        //
        OnCreated();
    }


    private void FixedUpdate()
    {
        if (hasBegun)
        {
            runTime += Time.deltaTime;
            //
            Run();
            //
            if (!hasEnded)
            {
                if (runTime >= expiresDate)
                {
                    End();
                }
            }
        }
        //
        if (hasEnded)
        {
            endTime += Time.deltaTime;
        }
    }


    /// <summary>
    /// Erases the FX.
    /// Override this function in order to "clean up" any other dependencies this has!
    /// </summary>
    public virtual void Discard()
    {
        Destroy(this.gameObject);
    }


    public virtual void Run() { }
    public virtual void OnCreated()
    {
        Begin();
    }
    public virtual void OnBegin() { }
    public virtual void OnEnded() { }


    [ClientRpc]
    public virtual void RpcOnServerSpawned()
    {
        OnCreated();
    }
}
