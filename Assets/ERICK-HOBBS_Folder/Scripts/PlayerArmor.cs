using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArmor : MonoBehaviour {

    public int maxArmor;
    public Text currentArmorText;
    private int currentArmor;

    public Text FakeHealthText;

    // Use this for initialization
    void Start () {

        currentArmor = maxArmor;
        UpdateGUI();
		
	}

    void UpdateGUI()
    {
        currentArmorText.text = currentArmor.ToString();
    }

    public void AlterArmor(int amount)
    {
        currentArmor += amount;
        currentArmor = Mathf.Clamp(currentArmor, 0, maxArmor);
        UpdateGUI();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.H))
        {
            AlterArmor(-10);
        }


        if (currentArmor == 0)
        {
            FakeHealthText.gameObject.SetActive(false);
            GetComponent<PlayerHealth>().enabled = true;
        }

    }
}
