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

    private float currentSpeed = 0.0f;

    private Vector2 faceDirection;
    private Vector2 moveDirection;
    private Vector2 mousePosition;

    /* USED FOR PLAYER MODEL ANIMATION */
    public Animator playerAnimator;
    public RectTransform crosshairRectTransform;

    [SerializeField] private Health healthScript;

    private bool slowed;

    [SerializeField]private float  takeDamageCoolDown;
    private float takeDamageTimer;


    void Start()
    {

        rigidBody = gameObject.GetComponent<Rigidbody2D>();


        if (SceneValues.earlierScene == "BuyShopScene")
        {
            oldPos = SceneValues.positionBeforeBuyShop;
            //problemet handlar kanske om att det är olika data? Detta är iallafall problemet. 

            transform.position = new Vector3(oldPos.x, oldPos.y, oldPos.z);


            //rigidBody.position = SceneValues.positionBeforeBuyShop.position;

        }

        //currentSpeed = 3.0f;

        // Confine and hide cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        playerAnimator.SetFloat("Speed", currentSpeed);

        // Rotating rectangle to mouse position
        //setting a direction for the rotation based on
        //transform and mouse position and then sets a crosshair to the positon of the mouse
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        faceDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = faceDirection;

        if (Input.GetKeyDown(KeyCode.H))
        {
            OnStart.coins += 1;
        }

        /* USED FOR SWITCHING ANIMATION STATES */
        if (moveDirection != Vector2.zero)
        {
            if (slowed == false)
            {
                currentSpeed = 3.0f;
            }
            else
            {
                currentSpeed = 1.0f;
            }
        }
        else
        {
            currentSpeed = 0.0f;
        }

        // GetAxis() returns a value of -1, 0 or 1 depending on button clicked, Which button does what can be seen under "input manager" in project settings
        // Its normalized so that the speed will be consistent even if you are walking diagonaly
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;

        takeDamageTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + moveDirection * currentSpeed * Time.fixedDeltaTime);

        /* SETS CROSSHAIR AT MOUSE POS */
        crosshairRectTransform.position = Input.mousePosition;

        //Sets crosshairs position to that of the mouse
        playerCrosshair.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (takeDamageTimer <= 0)
        {
            if (collisionObject.CompareTag("Zombie"))
            {
                healthScript.TakeDamage(collisionObject.GetComponent<ZombieAttack>().attackDamage);
                takeDamageTimer = takeDamageCoolDown;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (takeDamageTimer <= 0)
        {
            if (collisionObject.CompareTag("Enviromental Hazard"))
            {
                healthScript.TakeDamage(collisionObject.GetComponent<DamagingSpikes>().damage);
                takeDamageTimer = takeDamageCoolDown;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Slowing Area")
        {
            slowed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Slowing Area")
        {
            slowed = false;
        }
    }
}
