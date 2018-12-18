using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupManager : MonoBehaviour {

    private GameObject handGun;
    private GameObject LaserRifle;
    private GameObject SniperRifle;
    private GameObject Axe;
    private GameObject RocketLauncher;

    private GameObject RightHand;


    public List<GameObject> Weapons;

    private FPSController2 Controller;

	// Use this for initialization
	void Start () {

        Weapons = new List<GameObject>();

        Controller = GameObject.Find("Player").GetComponent<FPSController2>();

        AddWeaponToList();
        FindPlayerHand();

        for (int i = 0; i < Weapons.Count; i++)
        {
            Weapons[i].transform.position = RightHand.transform.position;
            Weapons[i].SetActive(false);
        }
        Weapons[0].SetActive(true);
    }

    private void AddWeaponToList()
    {
        handGun = (GameObject)Resources.Load("ERICK-HOBBS_Folder/Prefabs/RealWeapons/HandGun", typeof(GameObject));
        Weapons.Add(handGun);

        LaserRifle = (GameObject)Resources.Load("ERICK-HOBBS_Folder/Prefabs/RealWeapons/laserRifle_Real", typeof(GameObject));
        Weapons.Add(LaserRifle);

        SniperRifle = (GameObject)Resources.Load("ERICK-HOBBS_Folder/Prefabs/RealWeapons/SniperRifle_Real", typeof(GameObject));
        Weapons.Add(SniperRifle);

        Axe = (GameObject)Resources.Load("ERICK-HOBBS_Folder/Prefabs/RealWeapons/Axe_Real", typeof(GameObject));
        Weapons.Add(Axe);

        RocketLauncher = (GameObject)Resources.Load("ERICK-HOBBS_Folder/Prefabs/RealWeapons/rocketlauncher_Real", typeof(GameObject));
        Weapons.Add(RocketLauncher);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HandGun")
        {
            Weapons[0].SetActive(true);
            Controller.WeaponPickup[0] = true;
        }
        else if (other.gameObject.tag == "LaserRifle")
        {
            Weapons[1].SetActive(true);
            Controller.WeaponPickup[1] = true;
        }
        else if (other.gameObject.tag == "SniperRifle")
        {
            Weapons[2].SetActive(true);
            Controller.WeaponPickup[2] = true;
        }
        else if (other.gameObject.tag == "RocketLauncher")
        {
            Weapons[3].SetActive(true);
            Controller.WeaponPickup[3] = true;
        }
        else if (other.gameObject.tag == "Axe")
        {
            Weapons[4].SetActive(true);
            Controller.WeaponPickup[4] = true;
        }

    }

    private void FindPlayerHand()
    {
        RightHand = GameObject.Find("Player/model/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand");
    }

    private void CheckPlayerWeapon()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
