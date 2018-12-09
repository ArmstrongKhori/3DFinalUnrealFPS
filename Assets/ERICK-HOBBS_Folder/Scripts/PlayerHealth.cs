using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int maxHealth;
    public Text currentHealthText;
    public Image deadScreen;

    private int currentHealth;
    private bool isDead;

	// Use this for initialization
	void Start () {

        currentHealth = maxHealth;
        isDead = false;
        UpdateGUI();
		
	}

    void UpdateGUI()
    {
        currentHealthText.text = currentHealth.ToString();
        deadScreen.gameObject.SetActive(isDead);

    }

    public void AlterHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        CheckDead();
        UpdateGUI();

        
    }

    private void CheckDead()
    {
        if (isDead)
            return;

        if (currentHealth == 0)
        {
            isDead = true;
            GetComponent<FPSController>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.H))
        {
            AlterHealth(-10);
        }
       
    }
}
