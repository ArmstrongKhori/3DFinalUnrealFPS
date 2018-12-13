using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A basic camera that simply hovers.
/// </summary>
public class Cam : MonoBehaviour {

    internal Camera cam;
    private AudioListener al;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        al = GetComponent<AudioListener>();
    }



    public static Cam currentCam;
    public static void SetActiveCamera(Cam c)
    {
        if (currentCam != null) { currentCam.gameObject.SetActive(false); }
        //
        currentCam = c;
        //
        if (currentCam != null) { currentCam.gameObject.SetActive(true); }
    }

}
