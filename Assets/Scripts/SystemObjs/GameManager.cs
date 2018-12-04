using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
