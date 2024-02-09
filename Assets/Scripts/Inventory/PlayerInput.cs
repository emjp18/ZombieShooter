using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float interactionDistance = 2f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, interactionDistance);

            if(hit.collider != null )
            {
                PickUp pickUp = hit.collider.GetComponent<PickUp>();

                if(pickUp != null )
                {
                    pickUp.PickUpItem();
                }
            }
        }
    }
}
