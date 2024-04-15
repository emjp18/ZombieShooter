using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [SerializeField] private Transform playerTransfrom;

    private Rigidbody2D rigidBody;

    private Vector2 knockbackDirection;
    private float knockbackTimer;//Controls for how long the zombie will take knockback

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void TakeKnockBack(Vector2 direction)
    {
        knockbackDirection = direction;
        knockbackTimer = 0.07f;
    }

    private void FixedUpdate()
    {
        //Gets the vector that points from the zombie to the player
        Vector2 vectorToPlayer = playerTransfrom.position - gameObject.transform.position;

        if (knockbackTimer <= 0)
        {
            //Makes the zombie walk towards the player
            rigidBody.MovePosition(rigidBody.position + vectorToPlayer.normalized * movementSpeed * Time.fixedDeltaTime);
        }
        else if (knockbackTimer > 0)
        {
            //Makes the zombie take knockback
            rigidBody.MovePosition(rigidBody.position + knockbackDirection * Time.fixedDeltaTime);
            knockbackTimer -= Time.fixedDeltaTime;
        }

        //Makes the zombie face the player
        transform.up = vectorToPlayer;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rigidBody.velocity = Vector2.zero;
        }
    }*/
}
