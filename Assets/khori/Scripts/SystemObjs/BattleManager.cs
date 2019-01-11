using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BattleManager : SystemObj {


    public GameConditions gameConditions;




    internal GameObject gameSpace;
    internal List<Actor> allActors;



    public GameObject testObj;



    internal override void Initialize()
    {
        base.Initialize();
        //
        gameSpace = new GameObject("(gamespace)"); //  GameObject.Find("(gamespace)");
        allActors = new List<Actor>();

        gameConditions = new GameConditions();


        foreach (Actor a in FindObjectsOfType<Actor>())
        {
            allActors.Add(a);
        }


        testObj = (GameObject)Resources.Load("Spawnables/testpref", typeof(GameObject));



        gameConditions.Match_Begin();
    }


    private void FixedUpdate()
    {
        Run();
        //
        if (gameConditions.matchTime < gameConditions.matchDuration)
        {
            gameConditions.matchTime += Time.deltaTime;
            //
            if (gameConditions.matchTime >= gameConditions.matchDuration)
            {
                Helper.GetHostPlayer().PCH.CmdConcludeMatch("Time is up!");
            }
        }
    }


    /// <summary>
    /// The cycle of gameplay.
    /// Calls the "Run" event of all entities in the scene.
    /// </summary>
    public void Run()
    {
        // ??? <-- This is INCREDIBLY inefficient. Add proper list-modication later...
        Actor[] runningList = allActors.ToArray();
        foreach (Actor a in runningList)
        {
            a.Act();
        }
        //
        foreach (Actor a in runningList)
        {
            a.LateAct();
        }
    }


    public Actor Spawn(string name, Actor by)
    {
        return Spawn(name, by, Vector3.zero);
    }
    public Actor Spawn(string name, Actor by, Vector3 lookingVector)
    {
        Transform trans;
        if (by is Character) { trans = ((Character)by).gunPoint.transform; }
        else if (by != null) { trans = by.transform; }
        else { trans = gameSpace.transform; }


        Debug.Log("spawning: " + name);
        Object o = Resources.Load("Spawnables/" + name); // , typeof(Actor)
        Actor a = ((GameObject)Instantiate(o, trans.position, trans.rotation)).GetComponent<Actor>();
        a.Owner = by.netId;
        //
        a.transform.parent = null; //  gameSpace.transform;
        //
        //
        a.OnSpawned(lookingVector == Vector3.zero ? by.LookVector : lookingVector);


        return a;
    }


    
    public void RegisterPlayer(ControllableCharacter c)
    {
        // ??? <-- Does nothing atm...
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



public class GameConditions
{
    private BattleManager bm;

    public enum GameModes
    {
        /// <summary>
        /// The match will run indefinitely.
        /// </summary>
        None,
        /// <summary>
        /// The winner is the first to reach [threshold] kills.
        /// </summary>
        FreeForAll,
    }
    public GameModes gameMode;


    /// <summary>
    /// The required number of "points" required in the appropriate context.
    /// </summary>
    public int pointThreshold = 20;



    /// <summary>
    /// The length of the match (in seconds).
    /// </summary>
    public float matchDuration = 600.0f;

    internal float matchTime = 0.0f;


    public bool matchInitiated = false;
    public bool matchConcluded = false;



    public GameConditions() {
        bm = BattleManager.Instance();


        gameMode = GameModes.FreeForAll;
    }


    public void Match_Begin()
    {
        if (matchInitiated) { return; }


        if (bm.isServer)
        {

        }


        matchInitiated = true;
    }
    public void Match_End()
    {
        if (matchConcluded) { return; }


        matchConcluded = true;
    }
}
