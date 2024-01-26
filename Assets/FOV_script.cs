using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV_script : MonoBehaviour
{

    //Creates a Raycast
    RaycastHit2D hit;

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
        hit = Physics2D.Raycast(transform.position, transform.up,60f);
        if (hit.collider)
        {
            Debug.Log(hit.collider);
        }
        
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, Vector2.right);
        Gizmos.color = Color.green;
        
    }
}
