using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeDamageScript : MonoBehaviour
{
    BoxCollider2D box;
    Rigidbody2D rb;
    public float pushbackForce = 2.5f;
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bullet")
        {

            rb.AddForce((collision.transform.position - GameObject.Find("Player").transform.position).normalized * pushbackForce,ForceMode2D.Impulse);
        }
    }
    
}
