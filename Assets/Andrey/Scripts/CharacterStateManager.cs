using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour {


    public GameObject[] characterState;

    //Enums ---->
    public enum MajorCharacterStates { ACTIVE, DEAD }
    public MajorCharacterStates e_majorCharacterStates;

    public enum ActiveStates { IDLE, WALK, SPRINT, JUMP }
    public ActiveStates e_activeState;

    public enum DeadStates { DEAD, CHOST }
    public DeadStates e_deadStates;



    void Start () {
		
	}
	

	void Update () {
		


	}
}
