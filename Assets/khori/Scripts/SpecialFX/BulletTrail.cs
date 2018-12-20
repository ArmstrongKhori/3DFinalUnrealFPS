using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletTrail : SpecialFX {


    [SyncVar]
    public Vector3 startPoint;
    [SyncVar]
    public Vector3 endPoint;
    [SyncVar]
    public float size;
    [SyncVar]
    public Color color;

    private LineRenderer lr;



    public override void Run()
    {
        base.Run();
        //
        Color fadecol = new Color(color.r, color.g, color.b, 1 - Helper.Longevity(runTime, 0.1f, expiresDate - 0.1f));
        lr.startColor = fadecol;
        lr.endColor = fadecol;
    }

    public override void OnCreated()
    {
        expiresDate = 0.3f;
        
        lr = gameObject.GetComponent<LineRenderer>();
        if (lr == null) { lr = gameObject.AddComponent<LineRenderer>(); }
        //
        //
        base.OnCreated();
    }


    public override void OnBegin()
    {
        base.OnBegin();
        //
        lr.material = (Material)Resources.Load("LineRendererMat", typeof(Material)); // ??? <-- Very hard-coded. Fix later...
        lr.startWidth = size;
        lr.endWidth = size;
        lr.startColor = Color.clear;
        lr.endColor = Color.clear;

        Vector3[] poses = new Vector3[2];
        poses[0] = startPoint;
        poses[1] = endPoint;
        lr.SetPositions(poses);
    }

}
