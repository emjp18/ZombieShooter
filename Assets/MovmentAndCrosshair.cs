using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;

public class MovementAndCrosshair : MonoBehaviour
{
    public Transform crosshair;
    public Camera playerCamera;

    private Rigidbody2D rb;

    private float currentSpeed;

    private Vector2 faceDirection;
    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //rb.gravityScale = 0.0f;

        currentSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotating rectangle to mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        faceDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = faceDirection;
        crosshair.position = new Vector3 (mousePosition.x,mousePosition.y,0);

        // GetAxis() returns a value of -1, 0 or 1 depending on button clicked, Which button does what can be seen under "input manager" in project settings
        // Its normalized so that the speed will be consistent even if you are walking diagonaly
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;

        // Camera
        playerCamera.transform.position = new Vector3(transform.position.x,transform.position.y,-10);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);

    }
}
