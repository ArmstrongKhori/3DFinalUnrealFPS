using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This singleton deals with all things related to sound (such as sound effects, music, and whatnot.)
/// </summary>
public class Sounder : SystemObj {
    


    #region Singleton Stuff
    private static Sounder _instance;
    public static Sounder Instance() { return _instance; }
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
