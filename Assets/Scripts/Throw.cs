using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{

    public float throwForce = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Ensure the z-coordinate is at the same level as the bomb

            Vector2 throwDirection = (mousePosition - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        }
    }
}
