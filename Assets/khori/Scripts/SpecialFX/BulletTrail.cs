using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : SpecialFX {


    public Vector3 startPoint;
    public Vector3 endPoint;
    public float size;
    public Color color;

    private LineRenderer lr;



    public static BulletTrail Create(Vector3 start, Vector3 end, float sz, Color col)
    {
        GameObject go = new GameObject();
        //
        BulletTrail comp = go.AddComponent<BulletTrail>();
        comp.startPoint = start;
        comp.endPoint = end;
        comp.size = sz;
        comp.color = col;
        //
        comp.OnCreated();
        //
        //
        return comp;
    }



    public override void Run()
    {
        base.Run();
        //
        Color fadecol = new Color(color.r, color.g, color.b, 1 - Helper.Longevity(runTime, 1.0f, expiresDate - 1.0f));
        lr.startColor = fadecol;
        lr.endColor = fadecol;
    }

    public override void OnCreated()
    {
        expiresDate = 2.0f;


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
