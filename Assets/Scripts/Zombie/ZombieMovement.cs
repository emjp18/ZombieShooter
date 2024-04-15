using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [SerializeField] private Transform playerTransfrom;

    [SerializeField] private Animator animator;
    [SerializeField] private bool isWalking = false;
    [SerializeField] private bool isAttacking = false;

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
        Vector2 vectorToPlayer = playerTransfrom.position - transform.position;

        if (knockbackTimer > 0)
        {
            // Makes the zombie take knockback
            rigidBody.MovePosition(rigidBody.position + knockbackDirection * Time.fixedDeltaTime);
            knockbackTimer -= Time.fixedDeltaTime;
            animator.SetBool("isWalking", false); // Ensure walking is disabled during knockback
        }
        else
        {
            if (vectorToPlayer.magnitude > 0.1f)
            {
                // Makes the zombie walk towards the player
                rigidBody.MovePosition(rigidBody.position + vectorToPlayer.normalized * movementSpeed * Time.fixedDeltaTime);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }

        // Makes the zombie face the player
        transform.up = vectorToPlayer;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        IsAttacking(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Optionally handle the collision impact here if needed
    }

    private void IsAttacking(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rigidBody.velocity = Vector2.zero;
        }
    }*/
}
