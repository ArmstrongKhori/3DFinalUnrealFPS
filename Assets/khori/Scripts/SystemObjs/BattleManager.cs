using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public Actor Spawn(string name, Character by)
    {
        Actor a = (Actor)Instantiate(Resources.Load("Spawnables/" + name, typeof(Actor)), by.gunPoint.transform);
        a.Owner = by;
        //
        a.transform.parent = gameSpace.transform; // *** Detach it from the target after spawning it ONTO the target.
        //
        //
        a.OnSpawned();


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
