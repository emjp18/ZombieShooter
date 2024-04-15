using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private float timerForHeal = 2.5f;

    public Healthbar healthBar;
    

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) 
        {
            currentHealth = 0;
            //death animation
            //game over screen
        }
    }

    public void Heal(int health)
    {
        currentHealth += health;
        healthBar.SetHealth(currentHealth);
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            //heal animation
            
        }
    }

    void HealingIntervall()
    {
        timerForHeal -= Time.deltaTime;
        if(timerForHeal <= 0)
        {
            timerForHeal = 2.5f;
            Heal(2);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        HealingIntervall();
    }
}
