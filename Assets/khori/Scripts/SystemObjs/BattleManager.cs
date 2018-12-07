using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : SystemObj {
    

    /// <summary>
    /// The cycle of gameplay.
    /// Calls the "Run" event of all entities in the scene.
    /// </summary>
    public void Run()
    {

    }


    #region Singleton Stuff
    private static BattleManager _instance;
    public static BattleManager Instance() { return _instance; }
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
