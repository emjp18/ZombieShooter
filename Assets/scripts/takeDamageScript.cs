using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeDamageScript : MonoBehaviour
{
    BoxCollider2D box;
    Rigidbody2D rb;
    public float pushbackForce = 2.5f;

    public int healthPoints = 100;
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
        if (healthPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            //Debug.Log("hit");
            int damage = collision.gameObject.GetComponent<weapon_DamageScript>().damagePerHit;
            healthPoints -= damage;

            rb.AddForce((collision.transform.position - GameObject.Find("Player").transform.position).normalized * pushbackForce,ForceMode2D.Impulse);
        }
    }
    
}
