using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;

public class MovementAndCrosshair : MonoBehaviour
{
    private CharacterController controller;
    private float legacySpeed;
    public Transform crosshair;
    public Camera playerCamera;
    Vector3 direction;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        speed = 10f;
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotating rectangle to mouse position
        //Sets 
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
        crosshair.position = new Vector3 (mousePosition.x,mousePosition.y,0);

        //Move code
        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        //Camera
        playerCamera.transform.position = new Vector3(transform.position.x,transform.position.y,-10);
    }

    //Old movement code i am not sure i wanna remove yet
    void LegacyMovement()
    {
        //Move with wasd and arrow keys
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        move.Normalize();
        controller.Move(move * Time.deltaTime * legacySpeed);
    }

}
