using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;

public class CollisionMovement : MonoBehaviour
{
    public Transform playerCrosshair;

    public Transform thePlayerPosition; 

    public Camera camera;

    private (float x, float y, float z) oldPos; 

    private Rigidbody2D rigidBody;

    private float currentSpeed;

    private Vector2 faceDirection;
    private Vector2 moveDirection;
    private Vector2 mousePosition;

    void Start()
    {

        rigidBody = gameObject.GetComponent<Rigidbody2D>();


        if (SceneValues.earlierScene == "BuyShopScene")
        {
            oldPos = SceneValues.positionBeforeBuyShop; 
            //problemet handlar kanske om att det �r olika data? Detta �r iallafall problemet. 

            transform.position = new Vector3(oldPos.x, oldPos.y, oldPos.z ); 


            

            //rigidBody.position = SceneValues.positionBeforeBuyShop.position;

        }

        currentSpeed = 5.0f;

        // Confine and hide cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        
    }

    void Update()
    {
        // Rotating rectangle to mouse position
        //setting a direction for the rotation based on
        //transform and mouse position and then sets a crosshair to the positon of the mouse
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        faceDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = faceDirection;

        // GetAxis() returns a value of -1, 0 or 1 depending on button clicked, Which button does what can be seen under "input manager" in project settings
        // Its normalized so that the speed will be consistent even if you are walking diagonaly
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + moveDirection * currentSpeed * Time.fixedDeltaTime);
        
        //Sets crosshairs position to that of the mouse
        playerCrosshair.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }
}
