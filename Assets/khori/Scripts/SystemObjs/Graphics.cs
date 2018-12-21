using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphics : SystemObj {

    public Texture screenTex;

    

    internal override void Initialize()
    {
        base.Initialize();
        //
        screenTex = (Texture)Resources.Load("screenTex", typeof(Texture));
    }



    #region Singleton Stuff
    private static Graphics _instance;
    public static Graphics Instance() { return _instance; }
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
