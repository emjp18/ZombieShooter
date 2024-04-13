using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransfrom;

    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Gets the vector that points from the zombie to the player
        Vector2 vectorToPlayer = playerTransfrom.position - gameObject.transform.position;

        //Makes the zombie walk towards the player
        rigidBody.MovePosition(rigidBody.position + vectorToPlayer.normalized * 1.5f * Time.fixedDeltaTime);

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
