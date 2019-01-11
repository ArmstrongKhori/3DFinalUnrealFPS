using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth2 : NetworkBehaviour
{

    public float maxHealth = 100;
    public Text currentHealthText;
    public Image deadScreen;

    private float currentHealth;
    public bool isDead;


    // Use this for initialization
    void Start()
    {
        InitializeMe();
        //
        UpdateGUI();
    }

    public void InitializeMe()
    {
        currentHealth = maxHealth;
        isDead = false;
        //
        UpdateGUI();
    }


    void UpdateGUI()
    {
        if (isLocalPlayer)
        {
            currentHealthText.text = currentHealth.ToString("f00");
            // deadScreen.gameObject.SetActive(isDead);
        }
        else
        {
            enabled = false;
        }
    }

    public void AlterHealth(float amount)
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


            GetComponent<Character>().Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
