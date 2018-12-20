using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BattleManager : SystemObj {



    internal GameObject gameSpace;
    internal List<Actor> allActors;



    public GameObject testObj;



    internal override void Initialize()
    {
        base.Initialize();
        //
        gameSpace = new GameObject("(gamespace)"); //  GameObject.Find("(gamespace)");
        allActors = new List<Actor>();


        foreach (Actor a in FindObjectsOfType<Actor>())
        {
            allActors.Add(a);
        }


        testObj = (GameObject)Resources.Load("Spawnables/testpref", typeof(GameObject));
    }


    private void FixedUpdate()
    {
        Run();
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


        Actor a = (Actor)Instantiate(Resources.Load("Spawnables/" + name, typeof(Actor)), trans.position, trans.rotation);
        a.Owner = by.netId;
        //
        a.transform.parent = null; //  gameSpace.transform;
        //
        //
        a.OnSpawned(lookingVector == Vector3.zero ? by.LookVector : lookingVector);


        return a;
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



public class ParticipantData
{
    public int kills = 0;
    public int deaths = 0;
    public Character character = null;
}
