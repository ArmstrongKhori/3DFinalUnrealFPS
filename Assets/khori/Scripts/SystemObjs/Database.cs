using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : SystemObj {


    // ??? <-- TODO


    #region Singleton Stuff
    private static Database _instance;
    public static Database Instance() { return _instance; }
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
