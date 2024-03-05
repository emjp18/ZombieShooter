using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{

    public float throwForce = 10f;

    private Vector3 initialPosition;


    void Start()
    {
        // Disable the renderer initially
        GetComponent<Renderer>().enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowItem();
        }

    }

    public void ThrowItem()
    {

        GetComponent<Renderer>().enabled = true;

        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = playerPosition;

        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate the throw direction
        Vector2 throwDirection = (mousePosition - playerPosition).normalized;

        // Get the Rigidbody2D component of the item and add force to it
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

    }
}
