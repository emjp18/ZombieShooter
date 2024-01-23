using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;

public class MovementAndCrosshair : MonoBehaviour
{
    private CharacterController controller;
    private float playerSpeed = 10.0f;
    public Transform crosshair;
    public Camera playerCamera;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotating rectangle to mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
        crosshair.position = new Vector3 (mousePosition.x,mousePosition.y,0);

        //Move with wasd and arrow keys
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        move.Normalize();
        controller.Move(move * Time.deltaTime * playerSpeed);

        //Camera
        playerCamera.transform.position = new Vector3(transform.position.x,transform.position.y,-10);
    }


    
}
