using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    public Transform crosshairTransform;
    private new Rigidbody2D rigidbody;

    private Vector2 direction;
    [System.NonSerialized] public float timerUntilDestoyed;

    [System.NonSerialized] public int damage;
    [System.NonSerialized] public float speed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        //Normalizes the vector from the bullets position to the crosshairs position, to create a vector representing the direction the bullet should move
        direction = new Vector2(crosshairTransform.position.x - transform.position.x, crosshairTransform.position.y - transform.position.y).normalized;
        //speed = 100;
    }

    void Update()
    {
        timerUntilDestoyed -= Time.deltaTime;
        if (timerUntilDestoyed <= 0)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Collision (Wall)" || other.gameObject.tag == "zombie")
        {
            Destroy(gameObject);
        }
    }
}
