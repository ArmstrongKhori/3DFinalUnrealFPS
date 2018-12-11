using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The "GameManager" is the place where all behind-the-scenes game management happens (as the name implies.)
/// 
/// </summary>
public class GameManager : SystemObj {





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
}
