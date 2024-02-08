using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Healthbar bar;
    

    void Start()
    {
        currentHealth = maxHealth;
        bar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        bar.SetHealth(currentHealth);
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
        bar.SetHealth(currentHealth);
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            //heal animation
            
        }
    }

    


}
