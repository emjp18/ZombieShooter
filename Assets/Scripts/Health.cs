using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) 
        {
            currentHealth = 0;
            //death animation
            //game over screen
        }
    }

    void Heal(int health)
    {
        currentHealth += health;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            //heal animation
            
        }
    }
}
