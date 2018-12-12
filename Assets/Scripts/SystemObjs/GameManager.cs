using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The "GameManager" is the place where all behind-the-scenes game management happens (as the name implies.)
/// 
/// </summary>
public class GameManager : SystemObj {

    public override void Start()
    {
        base.Start();
        //
        // *** Call "Initialize" for all SystemObjs that currently exist (except this.)
        Initialize();
        //
        foreach (SystemObj a in FindObjectsOfType<SystemObj>())
        {
            if (a == this) { continue; }
            
            a.Initialize();
        }
    }



    #region Singleton Stuff
    private static GameManager _instance;
    public static GameManager Instance() { return _instance; }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        //
        DontDestroyOnLoad(this.gameObject);


        Initialize();
    }
    #endregion

}
