﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The "GameManager" is the place where all behind-the-scenes game management happens (as the name implies.)
/// 
/// </summary>
public class GameManager : SystemObj {


    public List<GameObject> networkSpawnablePrefabs;


    public override void Start()
    {
        base.Start();
        //
        GameObject go;
        go = new GameObject("BattleManager");
        go.transform.parent = transform;
        go.AddComponent<BattleManager>();

        go = new GameObject("Networker");
        go.transform.parent = transform;
        go.AddComponent<Networker>();
        //
        //
        /*
        foreach (SystemObj a in FindObjectsOfType<SystemObj>())
        {
            if (a == this) { continue; }
            
            a.Initialize();
        }
        */
    }

    internal Canvas screenCanvas;
    internal Text screenText;
    internal override void Initialize()
    {
        base.Initialize();
        //
        GameObject go;
        go = new GameObject("__ScreenCanvas");
        screenCanvas = go.AddComponent<Canvas>();
        screenCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //
        if (screenCanvas != null)
        {
            go = new GameObject("__ScreenText");
            go.transform.parent = screenCanvas.transform;
            //
            screenText = go.AddComponent<Text>();
            screenText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            screenText.alignment = TextAnchor.MiddleCenter;
            screenText.fontSize = 27;
            screenText.color = Color.magenta;
            //
            screenText.rectTransform.anchorMin = new Vector2(0, 0);
            screenText.rectTransform.anchorMax = new Vector2(1, 1);
            //
            screenText.rectTransform.offsetMin = new Vector2(0, 0);
            screenText.rectTransform.offsetMax = new Vector2(0, 0);
        }
    }


    public void DisplayMessage(string message)
    {
        screenText.text = message;
    }
    public void AppendMessage(string message)
    {
        screenText.text += "\n" + message;
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
