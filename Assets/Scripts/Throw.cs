using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{

    public float throwForce = 10f;
    public KeyCode actionKey;
    public ActiveItem activeItemScript;
    public ThrowCooldown throwCooldownScript;
    public bool throwing = false;

    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private Collider2D coll;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        coll = GetComponent<Collider2D>();
        coll.enabled = true;
    }
   
    void Update()
    {
        if (Input.GetKeyDown(actionKey))
        {
            ThrowItem();
        }
        if (activeItemScript.inInventory)
        {
            coll.enabled = false;
        }
        else
        {
            coll.enabled = true;
        }


    }

    public void ThrowItem()
    {
        if (activeItemScript.inInventory && throwCooldownScript.canThrow)
        {
            throwing = true;

            throwCooldownScript.SetThrowCooldown();

            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            transform.position = playerPosition;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector2 throwDirection = (mousePosition - playerPosition).normalized;

            rb.isKinematic = false;
            rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

            activeItemScript.inInventory = false;

            Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());

            //===========remove from inventory===========
        }

    }
}
