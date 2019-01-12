using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A basic camera that simply hovers.
/// </summary>
public class Cam : MonoBehaviour {

    internal Camera Camera { get { return Camera.main; } }

    private void Awake()
    {
    }


    private void LateUpdate()
    {
        if (currentCam == this)
        {
            Record();
        }
    }


    public virtual void Record() { }


    public static Cam currentCam;
    public static void SetActiveCamera(Cam c)
    {
        if (currentCam != null) { currentCam.gameObject.SetActive(false); }
        //
        currentCam = c;
        //
        if (currentCam != null) { currentCam.gameObject.SetActive(true); }
    }



    private void OnLevelWasLoaded(int level)
    {
        SetActiveCamera(FindObjectOfType<Cam>());
    }

}
